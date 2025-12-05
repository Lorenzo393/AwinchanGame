using System.Collections;
using UnityEngine;

public class DrawerAnimation : MonoBehaviour, IInteractable
{
    private Vector3 closedPos;
    private Vector3 openedPos;
    private float openedLength = 0.376f;
    private float drawerSpeed = 2f;
    private bool isOpen = false;
    private bool isMoving = false;
    public void Interact(){
        if (!isMoving){
            if(isOpen == false){
                StartCoroutine(OpenCloseDrawer(closedPos, openedPos));
                isOpen = true;
            } else if(isOpen == true){
                StartCoroutine(OpenCloseDrawer(openedPos, closedPos));
                isOpen = false;
            }
        }
        
    }
    private void Start(){
        closedPos = transform.localPosition;
        openedPos = new Vector3(transform.localPosition.x, transform.localPosition.y, (transform.localPosition.z + openedLength));
        transform.localPosition = closedPos;
    }
    IEnumerator OpenCloseDrawer(Vector3 posInicial, Vector3 posFinal){
        isMoving = true;
        for(float t = 0f ; t <= 1.0f ; t += Time.deltaTime * drawerSpeed){
            transform.localPosition = Vector3.Lerp(posInicial, posFinal, t);
            yield return null;
        }
        isMoving = false;
    }
}
