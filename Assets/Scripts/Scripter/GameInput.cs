using System;
using System.Collections;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // Hago que solo pueda haber una instancia del game input
    public static GameInput Instance {get; private set;}
    // Se tiene que llamar igual que el script generado por el input system.
    private InputSystem inputSystem;
    // Evento correr iniciado
    public event EventHandler OnSprintActionStarted;
    // Evento correr cancelado
    public event EventHandler OnSprintActionCanceled;
    // Evento interaccion
    public event EventHandler OnInteractAction;
    // Evento linterna
    public event EventHandler OnFlashlightAction;
    private void Awake(){
        // Inicializo el singleton
        Instance = this;
        // Inicializo el script
        inputSystem = new InputSystem();

        // Tenemos que activar manualmente cada uno de los action maps.
        inputSystem.Player.Enable();

        // Seteo evento correr cuando se apreta el boton de sprint
        inputSystem.Player.Sprint.started += Sprint_started;
        // Seteo evento correr cuando se deja de apretar el boton de sprint
        inputSystem.Player.Sprint.canceled += Sprint_canceled;

        // Seteo evento interactuar cuando se apreta el bindeo sprint
        inputSystem.Player.Interact.performed += Interact_performed;

        // Seteo evento linterna
        inputSystem.Player.Flashlight.performed += Flashlight_performed;
    }

    public Vector2 GetMovementVectorNormalized(){
        // Lee el valor de Vector2 del action map move
        Vector2 inputVectorMovement = inputSystem.Player.Move.ReadValue<Vector2>();
        // Normaliza el vector haciendo que cuando camines en diagonal no ganes velocidad
        inputVectorMovement = inputVectorMovement.normalized;
        return inputVectorMovement;
    }

    private void Sprint_started(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnSprintActionStarted?.Invoke(this, EventArgs.Empty);
    }
    private void Sprint_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnSprintActionCanceled?.Invoke(this, EventArgs.Empty);
    }
    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnInteractAction?.Invoke(this, EventArgs.Empty);
    }
    private void Flashlight_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj){
        OnFlashlightAction?.Invoke(this, EventArgs.Empty);
    }
    public void BlockPlayerInput(){
        inputSystem.Player.Disable();
    }
    public void EnablePlayerInput(){
        inputSystem.Player.Enable();
    }
}
