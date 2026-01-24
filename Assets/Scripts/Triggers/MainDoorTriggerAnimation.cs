using System;
using UnityEngine;

public class MainDoorTriggerAnimation : MonoBehaviour, IInteractable
{
    public static MainDoorTriggerAnimation Instance {get; private set;}
    public event EventHandler OnMainDoorInteraction;
    [SerializeField] private AudioClip unlockDoorSound;
    [SerializeField] private AudioClip blockedDoorSound;
    [SerializeField] private Key.KeyTipe unlockKey;
    private void Awake(){
        Instance = this;
    }
    public void Interact(){
        if(unlockKey == PlayerInventory.Instance.GetCurrentKey()){
            SoundManager.Instance.PlaySound(unlockDoorSound, transform.position);
            PlayerInventory.Instance.RemoveKey(unlockKey);
            OnMainDoorInteraction?.Invoke(this, EventArgs.Empty);
        } else SoundManager.Instance.PlaySound(blockedDoorSound, transform.position);
    }
}
