using System.Collections;
using UnityEngine;

public class BlockedFence : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip fenceSound;
    [SerializeField] private string interactionText = "No puedo irme sin mi telefono";
    private bool canInteract = true;
    public void Interact(){
        if (canInteract) {
            SoundManager.Instance.PlaySound(fenceSound, transform.position);
            SituationTextUI.Instance.ShowText(interactionText);
            canInteract = false;
            StartCoroutine(ResetInteraction());
        }
    }

    IEnumerator ResetInteraction(){
        yield return new WaitForSecondsRealtime(2.0f);
        canInteract = true;
        yield return null;
    }
}
