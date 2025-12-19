using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class DoorAnimation : MonoBehaviour
{
    private DoorHandler doorHandler;
    private Quaternion closedDoorRot;
    private Quaternion openedDoorRot;
    private float doorSpeed = 2f;
    private bool isOpen = false;
    private bool isMoving = false;

    private void DoorHandler_OnDoorInteract(object sender, System.EventArgs e){
        if (!isMoving){
            if(!isOpen) StartCoroutine(OpenCloseDoor(closedDoorRot, openedDoorRot));
            else StartCoroutine(OpenCloseDoor(openedDoorRot, closedDoorRot));

            isOpen = !isOpen;
        }
    }
    private void Start(){
        doorHandler = GetComponent<DoorHandler>();

        // Evento interaccion con la puerta desbloqueada
        doorHandler.OnDoorInteract += DoorHandler_OnDoorInteract;
        
        // Seteo basico de la puerta
        closedDoorRot = transform.localRotation;
        openedDoorRot = Quaternion.Euler(transform.localRotation.x, (transform.localRotation.y - 90), transform.localRotation.z);
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