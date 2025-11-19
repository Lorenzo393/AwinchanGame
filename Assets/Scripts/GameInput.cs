using System;
using System.Collections;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // Se tiene que llamar igual que el script generado por el input system.
    private InputSystem inputSystem;
    // Evento correr
    public event EventHandler OnSprintAction;
    // Evento interaccion
    public event EventHandler OnInteractAction;
    private void Awake(){
        // Inicializo el script
        inputSystem = new InputSystem();

        // Tenemos que activar manualmente cada uno de los action maps.
        inputSystem.Player.Enable();

        // Seteo evento correr cuando se apreta el bindeo sprint
        inputSystem.Player.Sprint.performed += Sprint_performed;
        // Seteo evento interactuar cuando se apreta el bindeo sprint
        inputSystem.Player.Interact.performed += Interact_performed;
    }

    public Vector2 GetMovementVectorNormalized(){
        // Lee el valor de Vector2 del action map move
        Vector2 inputVectorMovement = inputSystem.Player.Move.ReadValue<Vector2>();
        // Normaliza el vector haciendo que cuando camines en diagonal no ganes velocidad
        inputVectorMovement = inputVectorMovement.normalized;
        return inputVectorMovement;
    }

    private void Sprint_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnSprintAction?.Invoke(this, EventArgs.Empty);
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }

}
