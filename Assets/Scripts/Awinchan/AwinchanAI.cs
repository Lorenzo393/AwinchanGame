using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class AwinchanAI : MonoBehaviour
{
    private enum AwinchanStates{
        Disability,
        Roaming,
        Chasing,
        ChasingSpecial,
        Attack,
        MissPlayer,
    }
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
    [SerializeField] private float stopChasingDistance = 35f;
    [SerializeField] private float stopChasingTimer = 4f;
    [SerializeField] private float timeSinceLastSeen = 0f;
    
    private AwinchanStates awinchanState;
    private NavMeshAgent navMeshAgent;


    private void PickUpPhone_OpPickUpPhone(object sender, System.EventArgs e){
        TeleportAwinchan(firstPosition);

    }
    private void ChasingTrigger_OnChasingTriggerEnter(object sender, System.EventArgs e){
        StartCoroutine(StartingChasing());
    }
    private void StopTrigger_OnChasingTriggerEnter(object sender, System.EventArgs e){
        navMeshAgent.enabled = false;
        TeleportAwinchan(spawnPosition);
        navMeshAgent.enabled = true;
        awinchanState = AwinchanStates.Roaming;
    }
    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.enabled = false;

        TeleportAwinchan(hiddenPosition);
        awinchanState = AwinchanStates.Disability;

        roamDirection = GetRoamingPosition();
    }

    private void Start(){
        PickUpPhone.Instance.OnPickUpPhone += PickUpPhone_OpPickUpPhone;
        chasingTrigger.OnChasingTriggerEnter += ChasingTrigger_OnChasingTriggerEnter;
        stopTrigger.OnStopTriggerEnter += StopTrigger_OnChasingTriggerEnter;
    }
    private void Update(){
        Debug.DrawRay(transform.position, ((playerPosition.position - transform.position).normalized) * viewDistance, Color.red);
        switch (awinchanState){
            case AwinchanStates.Roaming:
                navMeshAgent.destination = roamDirection;
                direction.position = roamDirection;
                if(Vector3.Distance(transform.position, roamDirection) < reachedPositionDistance) roamDirection = GetRoamingPosition();
                if(PlayerDetected()) awinchanState = AwinchanStates.Chasing;

                break;

            case AwinchanStates.Chasing:
                navMeshAgent.destination = playerPosition.position;
                direction.position = playerPosition.position;
                if(Vector3.Distance(transform.position, playerPosition.position) < reachedPositionDistance){
                    Debug.Log("Attack");
                    //awinchanState = AwinchanStates.Attack;
                }
                if(Vector3.Distance(transform.position, playerPosition.position) > stopChasingDistance){
                    awinchanState = AwinchanStates.MissPlayer;
                }
                if (!PlayerDetected()){
                    timeSinceLastSeen += Time.deltaTime;
                    if(timeSinceLastSeen > stopChasingTimer){
                        timeSinceLastSeen = 0f;
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
                    Debug.Log("Attack");
                    //awinchanState = AwinchanStates.Attack;
                }
                break;

            case AwinchanStates.Attack:
                Debug.Log("Atacando");
                break;

            case AwinchanStates.MissPlayer:
                StartCoroutine(MissingPlayer());
                if(PlayerDetected()) {
                    awinchanState = AwinchanStates.Chasing;
                } else {
                    roamDirection = GetRoamingPosition();
                    awinchanState = AwinchanStates.Roaming; 
                }
                break;
            case AwinchanStates.Disability:
                break;

        }
    }
    IEnumerator StartingChasing(){
        yield return new WaitForSecondsRealtime(2.0f);
        navMeshAgent.enabled = true;
        awinchanState = AwinchanStates.ChasingSpecial;

        yield return null;
    }
    IEnumerator MissingPlayer(){
        yield return new WaitForSecondsRealtime(3.0f);
        // Animacion desconcertado
        yield return null;
    }
    
    private bool PlayerDetected(){
        Vector3 directionToPlayer = (playerPosition.position - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, playerPosition.position);
        bool playerDetected = false;

        if (distance < viewDistance)
        {
            float angle = Vector3.Angle(transform.forward, directionToPlayer);
            if (angle < viewAngle / 2f)
            {
                if (!Physics.Raycast(transform.position, directionToPlayer, distance, obstacleMask))
                {
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

}
