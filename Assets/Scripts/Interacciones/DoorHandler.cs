using System;
using UnityEngine;

public class DoorHandler : MonoBehaviour, IInteractable
{
    public event EventHandler OnDoorInteract;
    [SerializeField] private Key.KeyTipe unlockKey;
    [SerializeField] private bool unlockedDoor = false;
    public void Interact(){
        if (!unlockedDoor){
           if(unlockKey == PlayerInventory.Instance.GetCurrentKey()){
                unlockedDoor = true;
                PlayerInventory.Instance.RemoveKey(unlockKey);
                OnDoorInteract?.Invoke(this, EventArgs.Empty);
            } else Debug.Log("LLave incorrecta"); 
        } else OnDoorInteract?.Invoke(this, EventArgs.Empty);
    }
}
