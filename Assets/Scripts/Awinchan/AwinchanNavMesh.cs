using UnityEngine;
using UnityEngine.AI;

public class AwinchanNavMesh : MonoBehaviour
{
    [SerializeField] private Transform direction;
    private NavMeshAgent navMeshAgent;

    private void Awake(){
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        navMeshAgent.destination = direction.position;
    }
}
