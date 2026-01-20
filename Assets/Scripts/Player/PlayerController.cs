using System;
using System.Collections;
using Unity.Cinemachine;
using Unity.IntegerTime;
using UnityEngine;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{
    // Singleton
    public static PlayerController Instance {get; private set;}
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

    [Header ("Stamina")]
    [SerializeField] private float maxStamina = 5f;
    [SerializeField] private float staminaDrain = 1f;
    [SerializeField] private float staminaRegen = 0.5f;
    [SerializeField] private float staminaSprintThreshold = 1f;
    private float currentStamina;
    private bool blockSprint;
    private bool isSprinting;
    private bool isRunningFoward;

    [Header ("Smooth")]
    [SerializeField] private float movementSmoothTime = 0.1f;
    [SerializeField] private NoiseSettings noiseProfile;
    private Vector3 smoothMovement;
    private Vector3 smoothMovementVelocity; 

    private CinemachineBasicMultiChannelPerlin headBob;    

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

        // Enlazo el componenete de la camara
        headBob = camera.gameObject.GetComponent<CinemachineBasicMultiChannelPerlin>();
        // Configuro el perfil de ruido
        headBob.NoiseProfile = noiseProfile;
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

        // Casos en que puede correr
        isRunningFoward = inputVector.y > 0f;
        if(currentStamina <= 0f) blockSprint = true;
        if(currentStamina >= staminaSprintThreshold) blockSprint = false;

        // Si esta corriendo y tiene estamina corre y la consume, sino la regenera
        if (isSprinting && (currentStamina > 0f) && (!blockSprint) && isRunningFoward){
            currentSpeed = runningSpeed;
            ConsumeStamina();
        } else {
            currentSpeed = walkingSpeed;
            RegenerateStamina();
        }

        Vector3 targetMovement = playerMovement * currentSpeed;

        // Suaviza el movimiento en TODAS las direcciones
        smoothMovement = Vector3.SmoothDamp(smoothMovement, targetMovement, ref smoothMovementVelocity, movementSmoothTime);

        // Aplico gravedad sin que sea multiplicada por la velocidad del jugador
        smoothMovement.y = characterController.isGrounded? groundStickForce : gravityForce;

        // Mueve al personaje multiplicando su movimiento por deltaTime
        characterController.Move(smoothMovement * Time.deltaTime);
        
        if(inputVector == Vector2.zero){
            // Quieto
            headBob.AmplitudeGain = 1.1f;
            headBob.FrequencyGain = 0.3f;
        }
        if(inputVector != Vector2.zero && currentSpeed == walkingSpeed){
            // Caminando
            headBob.AmplitudeGain = 1.05f;
            headBob.FrequencyGain = 1f;
        }
        if(inputVector != Vector2.zero && currentSpeed == runningSpeed)
        {
            // Corriendo
            headBob.AmplitudeGain = 1f;
            headBob.FrequencyGain = 2f;
        }
        
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