using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitchInteraction : MonoBehaviour, IInteractable
{
    /*
    // Creo que tengo que mejorarlo no puedo tener un booleano para la animacion del switch y otro para prender o apagar las luces
    // Creo que tengo que llevar esa informacion desde el evento en si mismo, igual no se como hacer para que cuando interacture
    // Cambie tambien
    */

    // Evento que se dispara cuando se interactua con el switch de la luz
    public event EventHandler OnClickSwitch;
    public void Interact()
    {
        OnClickSwitch?.Invoke(this, EventArgs.Empty);
    }
}
