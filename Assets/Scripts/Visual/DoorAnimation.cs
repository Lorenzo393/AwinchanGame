using NUnit.Framework;
using UnityEngine;

public class DoorAnimation : MonoBehaviour, IInteractable
{
    private bool isOpen = false;
    public void Interact(){
        if(isOpen == false){
            isOpen = true;
            OpenDoor();
        } else if(isOpen == true){
            isOpen = false;
            CloseDoor();
        }
    }
    private void Start(){
        CloseDoor();
    }
    private void CloseDoor(){
        transform.localRotation = Quaternion.Euler(0,0,0);
    }
    private void OpenDoor(){
        transform.localRotation = Quaternion.Euler(0,-90,0);
    }
}