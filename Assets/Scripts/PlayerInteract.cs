using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Referencia al objeto que tiene el script gameInput
    [SerializeField] private GameInput gameInput;
    // Referencia de la camara para ver hacia donde esta viendo el jugador
    [SerializeField] private new Transform camera;
    // Capa en las que puede interactuar el jugador
    [SerializeField] private LayerMask interactableLayerMask;

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        PlayerInteraction();
    }

    private void Start(){
        // Evento interaccion
        gameInput.OnInteractAction += GameInput_OnInteractAction;
    }
    private void PlayerInteraction(){
        float interactDistance = 3.0f;
        if(Physics.Raycast(camera.position,camera.forward,out RaycastHit raycastHit, interactDistance, interactableLayerMask)){
            IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
            if (interactable != null) interactable.Interact();
            else Debug.Log("No es interactuable");
        }
    }
}
