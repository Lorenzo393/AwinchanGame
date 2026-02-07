using System.Collections;
using System.Collections.Generic;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class AwinchanAI : MonoBehaviour
{
    public static AwinchanAI Instance {get; private set;}
    private enum AwinchanStates{
        Disability,
        Roaming,
        Chasing,
        ChasingSpecial,
        Attack,
        MissPlayer,
    }
    [SerializeField] private Transform awinchanFace;
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform direction;
    [Header ("SpotsPositions")]
    [SerializeField] private Transform hiddenPosition;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform spawnPosition;
    [Header ("RoamingPositions")]
    [SerializeField] List<Transform> roamingPositionsList;
    [SerializeField]private Vector3 roamDirection;
    [SerializeField] private float reachedPositionDistance = 2f;
    [Header ("Triggers")]
    [SerializeField] private AwinchanChasingTrigger chasingTrigger;
    [SerializeField] private AwinchanStopTrigger stopTrigger;
    [Header ("Tracking player")]
    [SerializeField] private Transform playerPosition;
    [SerializeField] private LayerMask obstacleMask;
    private float viewDistance = 20f;
    private float viewAngle = 60f;
    [Header ("Loosing Player")]
    [SerializeField] private float stopChasingDistance = 50f;
    [SerializeField] private float stopChasingTimer = 3.5f;
    [SerializeField] private float timeSinceLastSeen = 0f;

    [Header ("Sounds")]
    [SerializeField] private List<AudioClip> footstepsSoundsList;
    [SerializeField] private AudioSource awinchanAttack;
    private AudioSource audioSource;
    [SerializeField] private float walkStepInterval = 0.8f;
    [SerializeField] private float runStepInterval  = 1f;

    [SerializeField] private float walkPitch = 0.9f;
    [SerializeField] private float runPitch  = 0.95f;

    private float footstepTimer;
    private bool wasRunning;
    
    private int soundListLength;
    
    private AwinchanStates awinchanState;
    private NavMeshAgent navMeshAgent;
    private Animator animator;
    private float minimumDetectionArea = 4f;
    private float runningSpeed = 12f;
    private float walkingSpeed = 8f;
    private float deathSpeed = 0f;
    private bool isKilling = false;


    private void PickUpPhone_OpPickUpPhone(object sender, System.EventArgs e){
        TeleportAwinchan(firstPosition);
    }
    private void ChasingTrigger_OnChasingTriggerEnter(object sender, System.EventArgs e){
        StartCoroutine(StartingChasing());
    }
    private void StopTrigger_OnStopTriggerEnter(object sender, System.EventArgs e){
        navMeshAgent.enabled = false;
        TeleportAwinchan(spawnPosition);
        navMeshAgent.enabled = true;
        navMeshAgent.speed = walkingSpeed;

        animator.SetBool("isRunning",false);
        animator.SetBool("isWalking",true);

        awinchanState = AwinchanStates.Roaming;
    }
    private void Awake(){
        Instance = this;

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;
        navMeshAgent.speed = walkingSpeed;

        animator = GetComponent<Animator>();

        TeleportAwinchan(hiddenPosition);
        awinchanState = AwinchanStates.Disability;

        roamDirection = GetRoamingPosition();
    }

    private void Start(){
        PickUpPhone.Instance.OnPickUpPhone += PickUpPhone_OpPickUpPhone;
        chasingTrigger.OnChasingTriggerEnter += ChasingTrigger_OnChasingTriggerEnter;
        stopTrigger.OnStopTriggerEnter += StopTrigger_OnStopTriggerEnter;

        audioSource = GetComponent<AudioSource>();
        soundListLength = footstepsSoundsList.Count;
    }
    private void Update(){
        //Debug.DrawRay(transform.position, ((playerPosition.position - transform.position).normalized) * viewDistance, Color.red);
        switch (awinchanState){
            case AwinchanStates.Roaming:
                navMeshAgent.destination = roamDirection;
                direction.position = roamDirection;
                if(Vector3.Distance(transform.position, roamDirection) < reachedPositionDistance) roamDirection = GetRoamingPosition();

                if(PlayerDetected()) TRoamingChasing();
                
                break;

            case AwinchanStates.Chasing:
                navMeshAgent.destination = playerPosition.position;
                direction.position = playerPosition.position;
                if(Vector3.Distance(transform.position, playerPosition.position) < reachedPositionDistance){
                    //Debug.Log("Attack");
                    awinchanState = AwinchanStates.Attack;
                }
                if(Vector3.Distance(transform.position, playerPosition.position) > stopChasingDistance){
                    animator.SetBool("isRunning",false);
                    navMeshAgent.speed = walkingSpeed;
                    awinchanState = AwinchanStates.MissPlayer;
                }
                if (!PlayerDetected()){
                    timeSinceLastSeen += Time.deltaTime;
                    if(timeSinceLastSeen > stopChasingTimer){
                        timeSinceLastSeen = 0f;
                        animator.SetBool("isRunning",false);
                        navMeshAgent.speed = walkingSpeed;
                        awinchanState = AwinchanStates.MissPlayer;
                    }
                } else {
                  timeSinceLastSeen = 0f;  
                }
                break;

            case AwinchanStates.ChasingSpecial:
                navMeshAgent.destination = playerPosition.position;
                direction.position = playerPosition.position;
                if(Vector3.Distance(transform.position, playerPosition.position) < reachedPositionDistance){
                    //Debug.Log("Attack");
                    awinchanState = AwinchanStates.Attack;
                }
                break;

            case AwinchanStates.Attack:
                animator.SetBool("isDeath",false);
                animator.SetBool("isWalking",false);
                animator.SetBool("isRunning",false);
                if (!isKilling){
                    isKilling = true;

                    StartCoroutine(AwinchanAttack());
                }
                
                break;

            case AwinchanStates.MissPlayer:
                StartCoroutine(MissingPlayer());
                if(PlayerDetected())TMissPlayerChasing();
                else TMissPlayerRoaming();
                break;
                
            case AwinchanStates.Disability:
                navMeshAgent.speed = deathSpeed;
                animator.SetBool("isDeath",true);
                break;

        }
        HandleAwinchanFootsteps_Time();
    }
    IEnumerator StartingChasing(){
        navMeshAgent.enabled = true;
        yield return new WaitForSecondsRealtime(1f);
        navMeshAgent.speed = runningSpeed;

        animator.SetBool("isDeath",false);
        animator.SetBool("isRunning",true);

        awinchanState = AwinchanStates.ChasingSpecial;

        yield return null;
    }
    IEnumerator MissingPlayer(){
        yield return new WaitForSecondsRealtime(3.0f);
        yield return null;
    }
    
    private bool PlayerDetected(){
        Vector3 directionToPlayer = (playerPosition.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        bool playerDetected = false;

        if (distance < minimumDetectionArea) playerDetected = true;

        if (distance < viewDistance){
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < viewAngle / 2f){
                if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask)){
                    playerDetected = true;
                }
            }
        }
        return playerDetected;
    }
    private void TeleportAwinchan(Transform spot){
        this.transform.SetPositionAndRotation(spot.position, spot.rotation);
    }

    private Vector3 GetRoamingPosition(){
        return roamingPositionsList[Random.Range(0,roamingPositionsList.Count)].position;
    }

    private void TRoamingChasing(){
        animator.SetBool("isWalking",false);
        animator.SetBool("isRunning",true);

        navMeshAgent.speed = runningSpeed;
        awinchanState = AwinchanStates.Chasing;
    }

    private void TMissPlayerRoaming(){
        roamDirection = GetRoamingPosition();
        animator.SetBool("isWalking",true);
        navMeshAgent.speed = walkingSpeed;
        awinchanState = AwinchanStates.Roaming; 
    }

    private void TMissPlayerChasing(){
        animator.SetBool("isRunning",true);
        navMeshAgent.speed = runningSpeed;
        awinchanState = AwinchanStates.Chasing;
    }

    public void DisabilityAwinchan(){
        animator.SetBool("isWalking",false);
        animator.SetBool("isRunning",false);
        awinchanState = AwinchanStates.Disability;
    }

    private void HandleAwinchanFootsteps_Time(){
        if (awinchanState == AwinchanStates.Disability || awinchanState == AwinchanStates.Attack) {
            footstepTimer = 0f;
            wasRunning = false;
            return;
        }

        if (navMeshAgent.velocity.sqrMagnitude < 0.1f){
            footstepTimer = 0f;
            return;
        }

        bool isRunning = awinchanState == AwinchanStates.Chasing || awinchanState == AwinchanStates.ChasingSpecial;

        if (isRunning != wasRunning){
            footstepTimer = 0f;
            wasRunning = isRunning;
        }

        float interval = isRunning ? runStepInterval : walkStepInterval;
        float pitch    = isRunning ? runPitch : walkPitch;

        footstepTimer += Time.deltaTime;

        if (footstepTimer >= interval){
            audioSource.pitch = pitch + UnityEngine.Random.Range(-0.05f, 0.05f);
            audioSource.PlayOneShot(footstepsSoundsList[UnityEngine.Random.Range(0, soundListLength)]);

            footstepTimer = 0f;
        }
    }

    IEnumerator AwinchanAttack(){
        awinchanAttack.enabled = true;

        CinemachineCamera playerVCam = playerCamera.GetComponent<CinemachineCamera>();
        GameInput.Instance.BlockCameraInput();
        GameInput.Instance.BlockPlayerInput();
        //playerCamera.Follow = awinchanFace;
        playerVCam.LookAt = awinchanFace;
        
        Destroy(playerCamera.GetComponent<CinemachinePanTilt>());
        playerVCam.AddComponent<CinemachineHardLookAt>();
        ShowHideHud.Instance.Hide();
        
        
        yield return new WaitForSecondsRealtime(1.3f);
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(1.8f);
        GameInput.Instance.EnableCameraInput();
        GameInput.Instance.EnablePlayerInput();
        CursorLock.Instance.EnableCursor();
        SceneManager.LoadScene(0);
    }
}