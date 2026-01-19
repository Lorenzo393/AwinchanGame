using UnityEngine;

public class PrincipalDoorAnimation : MonoBehaviour
{
    [SerializeField] private Transform rightDoor;
    [SerializeField] private Transform leftDoor;
    private DoorHandler doorHandler;

    private void DoorHandler_OnDoorInteract(object sender, System.EventArgs e){
        
    }

    private void Awake(){
        doorHandler = GetComponent<DoorHandler>();
    }
    private void Start(){
        doorHandler.OnDoorInteract += DoorHandler_OnDoorInteract;
    }

}
