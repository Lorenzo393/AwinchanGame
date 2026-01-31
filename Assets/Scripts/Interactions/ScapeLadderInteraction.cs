using System.Collections;
using UnityEngine;

public class ScapeLadderInteraction : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip interactionSound;
    [SerializeField] private string interactionText = "No puedo irme aun";
    private float showingTime = 0.7f;
    private float displayTime = 1f;
    private float hidingTime = 0.7f;
    private bool canInteract = true;
    public void Interact(){
        if (canInteract) {
            SoundManager.Instance.PlaySound(interactionSound, transform.position);
            SituationTextUI.Instance.ShowText(interactionText, showingTime, displayTime, hidingTime);
            canInteract = false;
            StartCoroutine(ResetInteraction());
        }
    }
    IEnumerator ResetInteraction(){
        yield return new WaitForSecondsRealtime(showingTime + displayTime + hidingTime + 0.5f);
        canInteract = true;
    }
}
