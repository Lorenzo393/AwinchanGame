using System;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnDoorInteract;
    [SerializeField] private AudioClip unlockDoorSound;
    [SerializeField] private AudioClip blockedDoorSound;
    [SerializeField] private Key.KeyTipe unlockKey;
    [SerializeField] private bool unlockedDoor = false;
    public void Interact(){
        if (!unlockedDoor){
           if(unlockKey == PlayerInventory.Instance.GetCurrentKey()){
                SoundManager.Instance.PlaySound(unlockDoorSound, transform.position);
                unlockedDoor = true;
                PlayerInventory.Instance.RemoveKey(unlockKey);
                OnDoorInteract?.Invoke(this, EventArgs.Empty);
            } else SoundManager.Instance.PlaySound(blockedDoorSound, transform.position);
        } else OnDoorInteract?.Invoke(this, EventArgs.Empty);
    }
}
