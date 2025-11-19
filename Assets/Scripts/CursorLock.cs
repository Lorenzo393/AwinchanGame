using UnityEngine;

public class CursorLock : MonoBehaviour
{
    private void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
