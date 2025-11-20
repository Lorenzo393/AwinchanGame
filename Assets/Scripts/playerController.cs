using System;
using System.Collections;
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
    // Variable para saber si el jugador esta corriendo
    private float currentSpeed;
    private void GameInput_OnSprintActionStarted(object sender, System.EventArgs e)
    {
        currentSpeed = runningSpeed;
    }

    private void GameInput_OnSprintActionCanceled(object sender, System.EventArgs e)
    {
        currentSpeed = walkingSpeed;
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        Debug.Log("Interact");
    }
    private void Start(){
        // Inicializa el character controller
        characterController = GetComponent<CharacterController>();

        // Inicializo la velocidad de movimiento actual
        currentSpeed = walkingSpeed;

        // 
        gameInput.OnSprintActionStarted += GameInput_OnSprintActionStarted;
        // 
        gameInput.OnSprintActionCanceled += GameInput_OnSprintActionCanceled;
        // 
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update(){
        // Mueve al personaje
        HandleMovement();
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = (camera.forward * inputVector.y) + (camera.right * inputVector.x);


        // SimpleMove es como el move pero aplica gravedad y multiplica por Time.deltaTime
        characterController.SimpleMove(movementDirection * GetCurrentSpeed());
    }

    public float GetCurrentSpeed()
    {
        return currentSpeed;
    }

}
