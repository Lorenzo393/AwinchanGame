using NavKeypad;
using UnityEngine;

public class UnlockSafeDoor : MonoBehaviour
{
    [SerializeField] private Keypad keyPad;
    [SerializeField] private DoorHandler doorHandler;
    [SerializeField] private AudioClip unlockSound;

    private void Start(){
        keyPad.OnAccessGranted += KeyPad_OnAccesGranted;
    } 

    private void KeyPad_OnAccesGranted(object sender, System.EventArgs e){
        doorHandler.unlockedDoor = true;
        SoundManager.Instance.PlaySound(unlockSound, transform.position);
    }
}
