using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInteract : MonoBehaviour
{
    public static PlayerInteract Instance {get; private set;}
    // Referencia de la camara para ver hacia donde esta viendo el jugador
    [SerializeField] private new Transform camera;
    // Capa en las que puede interactuar el jugador
    [SerializeField] private LayerMask interactableLayerMask;
    // Distancia de interaccion
    [SerializeField] private float interactDistance = 2.5f;
    // Evento puede interactuar UI
    public event EventHandler OnCanInteract;
    // Evento no puede interactuar UI
    public event EventHandler OnCantInteract;

    private void GameInput_OnInteractAction(object sender, System.EventArgs e){
        PlayerInteraction();
    }

    private void Awake(){
        Instance = this;
    }
    private void Start(){
        // Evento interaccion
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void Update(){
        if(Physics.Raycast(camera.position,camera.forward,out RaycastHit raycastHit, interactDistance, interactableLayerMask)) OnCanInteract?.Invoke(this, EventArgs.Empty);
        else OnCantInteract?.Invoke(this,EventArgs.Empty);
    }
    private void PlayerInteraction(){
        if(Physics.Raycast(camera.position,camera.forward,out RaycastHit raycastHit, interactDistance, interactableLayerMask)){
            IInteractable interactable = raycastHit.collider.GetComponent<IInteractable>();
            OnCanInteract?.Invoke(this, EventArgs.Empty);

            if (interactable != null) interactable.Interact();
            else Debug.Log("No es interactuable");
        }
    }
}
