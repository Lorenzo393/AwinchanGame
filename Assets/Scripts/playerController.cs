using System.Numerics;
using Unity.Collections;
using Unity.VisualScripting;
using UnityEngine.UI;
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
    
    
    public float maxStamina = 120f;
    private float currentStamina;
    public float staminaDrainRate = 25f; // Xs
    public float staminaRegenRate = 15f; // Xs
    public Slider staminaSlider;
    public Image staminaFillImage;

    public float staminaRecoveryDelay = 1.25f;
    private float regenDelayTimer = 0f;
    private bool isRegenerating = true;
    void Start(){
        player = GetComponent<CharacterController>();
        currentStamina = maxStamina;
        if (staminaSlider != null)
            staminaSlider.maxValue = maxStamina;
    }

    void Update(){
        // Ground check
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDist, groundMask);
        if (isGrounded && velocity.y < 0) velocity.y = -2f;

        // Jumping
        if (Input.GetButtonDown("Jump") && isGrounded) velocity.y = Mathf.Sqrt(jumpH * -2f * gravity);
        velocity.y += gravity * Time.deltaTime;

        // Input
        hor = Input.GetAxis("Horizontal");
        ver = Input.GetAxis("Vertical");
        playerInput = transform.right * hor + transform.forward * ver;
        playerInput = UnityEngine.Vector3.ClampMagnitude(playerInput, 1);

        // Check if sprinting is allowed
        bool wantsToSprint = Input.GetKey(KeyCode.LeftShift) && playerInput.magnitude > 0;
        bool isSprinting = wantsToSprint && currentStamina > 0;

        float speed = isSprinting ? speedRun : speedNormal;

        // Handle stamina
        if (isSprinting){
            currentStamina -= staminaDrainRate * Time.deltaTime;
            regenDelayTimer = staminaRecoveryDelay;
            isRegenerating = false;
            if (currentStamina <= 0){
                currentStamina = 0;
                isSprinting = false;
            }
        }
        else{
            if(regenDelayTimer > 0)regenDelayTimer -= Time.deltaTime;
            else isRegenerating = true;
            if (isGrounded && isRegenerating && currentStamina < maxStamina){
                currentStamina += staminaRegenRate * Time.deltaTime;
                if (currentStamina > maxStamina) currentStamina = maxStamina;
            }
        }

        // UI Update
        if (staminaSlider != null)
            staminaSlider.value = currentStamina;
        if (staminaFillImage != null){
            float staminaPercent = currentStamina / maxStamina;
            if (staminaPercent > 0.5f) staminaFillImage.color = Color.green;
            else if (staminaPercent > 0.2f) staminaFillImage.color = Color.yellow;
            else staminaFillImage.color = Color.red;
        }

        // Movement
        player.Move(speed * Time.deltaTime * playerInput);
        player.Move(velocity * Time.deltaTime);
    }
}