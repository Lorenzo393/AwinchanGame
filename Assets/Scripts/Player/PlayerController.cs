using System;
using System.Collections;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController Instance{get; private set;}
    // Character controller
    private CharacterController characterController;
    // Velocidad a la que camina el personaje
    [SerializeField] private float walkingSpeed = 4.0f;
    // Velocidad a la que corre el personaje
    [SerializeField] private float runningSpeed = 8.0f;
    // Referencia de la camara para ver hacia donde esta viendo el jugador
    [SerializeField] private new Transform camera;
    // Velocidad actual del jugador
    private float currentSpeed;
    // Fuerza de la gravedad/ caida del personaje
    private const float gravityForce = -20.0f;
    // Fuerza de empuje normal/ fuerza que se aplica con el personaje tocando el suelo 
    private const float groundStickForce = -2.0f;

    // STAMINA
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaDrain = 1f;
    [SerializeField] private float staminaRegen = 0.5f;
    [SerializeField] private float staminaSprintThreshold = 1f;
    private float currentStamina;
    private bool blockSprint = false;
    private bool isSprinting = false;
    
    private void GameInput_OnSprintActionStarted(object sender, System.EventArgs e){
        isSprinting = true;
    }
    private void GameInput_OnSprintActionCanceled(object sender, System.EventArgs e){
        isSprinting = false;
    }
    private void Start(){
        // Inicializo la instancia
        Instance = this;

        // Inicializa el character controller
        characterController = GetComponent<CharacterController>();

        // Inicializo la estamina
        currentStamina = maxStamina;

        // Inicializo la velocidad de movimiento actual
        currentSpeed = walkingSpeed;

        // Evento correr iniciado
        GameInput.Instance.OnSprintActionStarted += GameInput_OnSprintActionStarted;
        // Evento correr terminado
        GameInput.Instance.OnSprintActionCanceled += GameInput_OnSprintActionCanceled;
    }
    private void Update(){
        // Mueve al personaje
        HandleMovement();
        StaminaBar.Instance.UpdateStamina(currentStamina, maxStamina);
    }
    private void HandleMovement(){
        Vector2 inputVector = GameInput.Instance.GetMovementVectorNormalized();
        Vector3 playerMovement = (camera.forward * inputVector.y) + (camera.right * inputVector.x);
        playerMovement.y = 0f;

        if(currentStamina <= 0f) blockSprint = true;
        if(currentStamina >= staminaSprintThreshold) blockSprint = false;


        // Si esta corriendo y tiene estamina corre y la consume, sino la regenera
        if ((isSprinting) && (currentStamina > 0f) && (!blockSprint)){
            currentSpeed = runningSpeed;
            ConsumeStamina();
        } else {
            currentSpeed = walkingSpeed;
            RegenerateStamina();
        }

        // Multiplico la direccion de movimiento con la velocidad actual
        playerMovement *= currentSpeed;

        // Aplico gravedad sin que sea multiplicada por la velocidad del jugador
        playerMovement.y = characterController.isGrounded? groundStickForce : gravityForce;

        // Mueve al personaje multiplicando su movimiento por deltaTime
        characterController.Move(playerMovement * Time.deltaTime);
    }
    private void ConsumeStamina(){
        currentStamina -= staminaDrain * Time.deltaTime;
        if (currentStamina < 0f) currentStamina = 0f;
    }

    private void RegenerateStamina(){
        currentStamina += staminaRegen * Time.deltaTime;
        if (currentStamina > maxStamina) currentStamina = maxStamina;
    }
    public void TeleportPlayer(Vector3 newPosition){
        characterController.enabled = false;
        transform.position = newPosition;
        characterController.enabled = true;
    }
}