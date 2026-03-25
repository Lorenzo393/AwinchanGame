using System.Collections;
using UnityEngine;

public class ProtectiveCaseAnimation : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform caseTransform;
    [SerializeField] private AudioClip failureSound;
    [SerializeField] private bool isOpen = false;
    private Vector3 initialPos;
    private Vector3 finalPos;
    private float animationSpeed = 1.5f;
    private bool isMoving = false;

    private void Start(){
        initialPos = caseTransform.localPosition;
        finalPos = new Vector3(caseTransform.localPosition.x, caseTransform.localPosition.y, -0.775f);
        caseTransform.localPosition = initialPos;
    }
    public void Interact(){
        if (!isMoving){
            if (!isOpen) StartCoroutine(OpenCloseCase(initialPos, finalPos));
            else StartCoroutine(OpenCloseCase(finalPos, initialPos));

            SoundManager.Instance.PlaySound(failureSound, transform.position);
            isOpen = !isOpen;
        }
    }

    IEnumerator OpenCloseCase(Vector3 initialPos, Vector3 finalPos){
        isMoving = true;
        for(float f = 0f ; f <= 1.0f ; f += Time.deltaTime * animationSpeed){
            caseTransform.localPosition = Vector3.Lerp(initialPos, finalPos, f);   
            yield return null;
        }
        isMoving = false;
    }

}
