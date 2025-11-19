using System.Collections;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    // Se tiene que llamar igual que el script generado por el input system.
    private InputSystem inputSystem;

    private void Awake(){
        // Inicializo el script
        inputSystem = new InputSystem();

        // Tenemos que activar manualmente cada uno de los action maps.
        inputSystem.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized(){
        // Lee el valor de Vector2 del action map move
        Vector2 inputVectorMovement = inputSystem.Player.Move.ReadValue<Vector2>();
        // Normaliza el vector haciendo que cuando camines en diagonal no ganes velocidad
        inputVectorMovement = inputVectorMovement.normalized;
        return inputVectorMovement;
    }
}
