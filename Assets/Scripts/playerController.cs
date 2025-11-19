using System.Collections;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    // Character controller
    private CharacterController characterController;

    // Referencia al objeto que tiene el script gameInput
    [SerializeField] private GameInput gameInput;

    // Velocidad a la que se mueve el personaje
    [SerializeField] private float speed = 7.0f;

    // Referencia de la camara para ver hacia donde esta viendo el jugador
    [SerializeField] private new Transform camera;

    // Vector2 que guarda el input
    private Vector2 inputVectorMovement;

    private void Start(){
        // Inicializa el character controller
        characterController = GetComponent<CharacterController>();
    }

    private void Update(){
        // Mueve al personaje
        HandleMovement();
    }

    private void HandleMovement(){
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 movementDirection = (camera.forward * inputVector.y) + (camera.right * inputVector.x);

        // SimpleMove es como el move pero aplica gravedad y multiplica por Time.deltaTime
        characterController.SimpleMove(movementDirection * speed);
    }

}
