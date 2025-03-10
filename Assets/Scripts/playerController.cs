using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float hor;
    private float ver;
    private UnityEngine.Vector3 playerInput;
    public float speedNormal = 5.0f; // REFERENCE VAL
    public float speedRun = 10.0f; // REFERENCE VAL
    public float jumpH = 3.0f; // REFERENCE VAL
    public CharacterController player;

    public Transform groundCheck;
    public LayerMask groundMask;
    public float groundDist = 0.3f; // REFERENCE VAL
    private bool isGrounded;

    public float gravity = -9.8f; // REFERENCE VAL
    private UnityEngine.Vector3 velocity;
    
    void Start(){
        player = GetComponent<CharacterController>();
    }
    void Update(){
        isGrounded = Physics.CheckSphere(groundCheck.position,groundDist,groundMask);
        if(isGrounded && velocity.y < 0) velocity.y = -2f;
        if(Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpH * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;

        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");

        playerInput = transform.right * hor + transform.forward * ver;
        playerInput = UnityEngine.Vector3.ClampMagnitude(playerInput,1);

        float speed = Input.GetKey(KeyCode.LeftShift) ? speedRun : speedNormal;

        player.Move(speed * Time.deltaTime * playerInput);
        player.Move(velocity*Time.deltaTime);
    }
}