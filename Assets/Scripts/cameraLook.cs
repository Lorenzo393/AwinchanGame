using System;
using UnityEngine;

public class CameraLook : MonoBehaviour
{
    private float xRotation;
    public float mouseSens; // 300
    public Transform player;
    void Start(){
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update(){
        float mouseX = Input.GetAxis("Mouse X") * mouseSens * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y")* mouseSens * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation,-90f,90f);

        transform.localRotation = Quaternion.Euler(xRotation,0f,0f);

        player.Rotate(UnityEngine.Vector3.up*mouseX);
    }
}
