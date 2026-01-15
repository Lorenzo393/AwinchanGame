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
        Attack,
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
    private float viewDistance = 15f;
    private float viewAngle = 60f;
    
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
                break;
            case AwinchanStates.Attack:
                Debug.Log("Atacando");
                break;
            case AwinchanStates.Disability:

                break;
        }
    }
    IEnumerator StartingChasing(){
        yield return new WaitForSecondsRealtime(2.0f);
        navMeshAgent.enabled = true;
        awinchanState = AwinchanStates.Chasing;

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
