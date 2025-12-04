using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class DoorAnimation : MonoBehaviour, IInteractable
{
    private Quaternion closedDoorRot;
    private Quaternion openedDoorRot;
    private float doorSpeed = 2f;
    private bool isOpen = false;
    private bool isMoving = false;
    public void Interact(){
        if (!isMoving){
            if(isOpen == false){
                StartCoroutine(OpenCloseDoor(closedDoorRot, openedDoorRot));
                isOpen = true;
            } else if(isOpen == true){
                StartCoroutine(OpenCloseDoor(openedDoorRot, closedDoorRot));
                isOpen = false;
            }
        }
    }
    private void Start(){
        closedDoorRot = transform.localRotation;
        openedDoorRot = Quaternion.Euler(transform.localRotation.x, (transform.localRotation.y -90), transform.localRotation.z);
        transform.localRotation = closedDoorRot;
    }
    IEnumerator OpenCloseDoor(Quaternion inicialRotation, Quaternion finalRotation){
        isMoving = true;
        for(float t = 0f ; t <= 1.0f ; t += Time.deltaTime * doorSpeed){
            transform.localRotation = Quaternion.Slerp(inicialRotation, finalRotation, t);
            yield return null;
        }
        isMoving = false;
    }
}