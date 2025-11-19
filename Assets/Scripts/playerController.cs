using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Character controller
    private CharacterController characterController;

    // Referencia al objeto que tiene el script gameInput
    [SerializeField] private GameInput gameInput;

    // Velocidad a la que se mueve el personaje
    [SerializeField] private float speed = 10.0f;

    // Vector2 que guarda el input
    private Vector2 inputVectorMovement;

    private void Start(){
        // Inicializa el character controller
        characterController = GetComponent<CharacterController>();
    }

    private void Update(){
        // Mueve al personaje
        HandleMovement();

        //Debug.Log(inputVectorMovement);
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = new Vector3(inputVector.x, 0f, inputVector.y);

        characterController.SimpleMove(movementDirection * speed);
    }

}
