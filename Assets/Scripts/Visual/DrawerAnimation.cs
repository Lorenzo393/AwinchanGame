using System.Collections;
using UnityEngine;

public class DrawerAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip openDrawerSound;
    [SerializeField] private AudioClip closeDrawerSound;
    private float audioVolume = 0.5f;
    private Vector3 closedPos;
    private Vector3 openedPos;
    private float openedLength = 0.376f;
    private float drawerSpeed = 2f;
    private bool isOpen = false;
    private bool isMoving = false;
    public void Interact(){
        if (!isMoving){
            if(!isOpen) {
                SoundManager.Instance.PlaySound(openDrawerSound, transform.position, audioVolume);
                StartCoroutine(OpenCloseDrawer(closedPos, openedPos));
            } else {
                SoundManager.Instance.PlaySound(closeDrawerSound, transform.position, audioVolume);
                StartCoroutine(OpenCloseDrawer(openedPos, closedPos));
            }
            
            isOpen = !isOpen;
        }
        
    }
    private void Start(){
        // Inicializacion de posiciones
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
