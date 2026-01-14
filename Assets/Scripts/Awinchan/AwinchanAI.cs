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
        Cheasing,
        Attack,
    }
    private AwinchanStates awinchanState;
    [SerializeField] private Transform direction;
    [Header ("SpotsPositions")]
    [SerializeField] private Transform hiddenPosition;
    [SerializeField] private Transform firstPosition;
    [SerializeField] private Transform spawnPosition;
    [Header ("RoamingPositions")]
    [SerializeField] List<Transform> roamingPositionsList;
    [SerializeField]private Vector3 roamDirection;
    [SerializeField] private float reachedPositionDistance = 2f;

    private NavMeshAgent navMeshAgent;
    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        navMeshAgent.enabled = false;
        TeleportAwinchan(hiddenPosition);
        awinchanState = AwinchanStates.Disability;

        roamDirection = GetRoamingPosition();
        StartCoroutine(Test());
    }
    private void Update(){
        switch (awinchanState){
            case AwinchanStates.Roaming:
                navMeshAgent.destination = roamDirection;
                //direction.position = roamDirection;
                if(Vector3.Distance(transform.position, roamDirection) < reachedPositionDistance){
                    roamDirection = GetRoamingPosition();
                }
                break;
            case AwinchanStates.Cheasing:
                break;
            case AwinchanStates.Attack:

                break;
            case AwinchanStates.Disability:
                break;
            
        }
    }

    IEnumerator Test(){

        yield return new WaitForSecondsRealtime(2.0f);
        TeleportAwinchan(firstPosition);
        navMeshAgent.enabled = true;

        yield return new WaitForSecondsRealtime(5.0f);
        navMeshAgent.enabled = false;
        TeleportAwinchan(spawnPosition);
        navMeshAgent.enabled = true;
        awinchanState = AwinchanStates.Roaming;

        yield return null;
    }
    
    private void TeleportAwinchan(Transform spot){
        this.transform.SetPositionAndRotation(spot.position, spot.rotation);
    }

    private Vector3 GetRoamingPosition(){
        return roamingPositionsList[Random.Range(0,roamingPositionsList.Count)].position;
    }

}
