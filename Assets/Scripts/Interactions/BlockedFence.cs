using System.Collections;
using UnityEngine;

public class BlockedFence : MonoBehaviour, IInteractable
{
    [SerializeField] private ContextManager contextManager;
    [SerializeField] private AudioClip fenceSound;
    [SerializeField] private string interactionText = "No puedo irme sin mi telefono";
    private float showingTime = 0.7f;
    private float displayTime = 1f;
    private float hidingTime = 0.7f;
    private bool canInteract = false;

    private void Start(){
        contextManager.OnCloseContextManager += ContextManager_OnCloseContextManager;
    }

    private void ContextManager_OnCloseContextManager(object sender, System.EventArgs e){
        canInteract = true;
        contextManager.OnCloseContextManager -= ContextManager_OnCloseContextManager;
    }
    public void Interact(){
        if (canInteract) {
            SoundManager.Instance.PlaySound(fenceSound, transform.position);
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
