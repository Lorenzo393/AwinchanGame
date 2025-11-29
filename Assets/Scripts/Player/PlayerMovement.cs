using System;
using System.Collections;
using Unity.IntegerTime;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Character controller
    private CharacterController characterController;
    // Referencia al objeto que tiene el script gameInput
    [SerializeField] private GameInput gameInput;
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
    
    private void GameInput_OnSprintActionStarted(object sender, System.EventArgs e){
        currentSpeed = runningSpeed;
    }
    private void GameInput_OnSprintActionCanceled(object sender, System.EventArgs e){
        currentSpeed = walkingSpeed;
    }
    private void Start(){
        // Inicializa el character controller
        characterController = GetComponent<CharacterController>();

        // Inicializo la velocidad de movimiento actual
        currentSpeed = walkingSpeed;

        // Evento correr iniciado
        gameInput.OnSprintActionStarted += GameInput_OnSprintActionStarted;
        // Evento correr terminado
        gameInput.OnSprintActionCanceled += GameInput_OnSprintActionCanceled;
    }
    private void Update(){
        // Mueve al personaje
        HandleMovement();
    }
    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 playerMovement = (camera.forward * inputVector.y) + (camera.right * inputVector.x);
        playerMovement.y = 0f;

        // Multiplico la direccion de movimiento con la velocidad actual
        playerMovement *= GetCurrentSpeed();

        // Aplico gravedad sin que sea multiplicada por la velocidad del jugador
        playerMovement.y = characterController.isGrounded? groundStickForce : gravityForce;

        // Mueve al personaje multiplicando su movimiento por deltaTime
        characterController.Move(playerMovement * Time.deltaTime);
    }
    private float GetCurrentSpeed(){
        return currentSpeed;
    }

}