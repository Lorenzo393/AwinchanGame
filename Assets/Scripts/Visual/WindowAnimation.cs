using System.Collections;
using UnityEngine;

public class WindowAnimation : MonoBehaviour, IInteractable
{
    private Vector3 initialPos;
    private Vector3 finalPos;
    private float animationSpeed = 1.0f;
    private bool isMoving = false;
    private bool isOpen = false;

    public void Interact(){
        if (!isMoving){
            if (!isOpen){
                isOpen = true;
                StartCoroutine(OpenCloseWindow(initialPos, finalPos));
            } else {
                isOpen = false;
                StartCoroutine(OpenCloseWindow(finalPos, initialPos));
            }
        }
    }
    private void Start(){
        // Seteo de posiciones
        initialPos = transform.localPosition;
        finalPos = new Vector3(-2.212f, transform.localPosition.y, transform.localPosition.z);
        transform.localPosition = initialPos;
    }
    IEnumerator OpenCloseWindow(Vector3 initialPos, Vector3 finalPos){
        isMoving = true;
        for(float f = 0f ; f <= 1.0f ; f += Time.deltaTime * animationSpeed){
            transform.localPosition = Vector3.Lerp(initialPos, finalPos, f);   
            yield return null;
        }
        isMoving = false;
    }
}
