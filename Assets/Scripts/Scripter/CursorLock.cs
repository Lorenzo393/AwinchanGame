using Unity.VisualScripting;
using UnityEngine;

public class CursorLock : MonoBehaviour
{
    public static CursorLock Instance {get; private set;}
    private void Start(){
        Instance = this;

        BlockCursor();
    }

    public void BlockCursor(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void UnblockCursor(){
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
