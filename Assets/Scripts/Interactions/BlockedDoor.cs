using UnityEngine;

public class BlockedDoor : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip blockedDoorSound;
    public void Interact(){
        SoundManager.Instance.PlaySound(blockedDoorSound, transform.position);
    }
}
