using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class LightSwitchInteraction : MonoBehaviour, IInteractable
{
    // Evento que se dispara cuando se interactua con el switch de la luz
    public event EventHandler<OnClickSwitchEventArgs> OnClickSwitch;
    // 
    public class OnClickSwitchEventArgs: EventArgs{
        public bool lightState;
    }
    // Encendido/apagado de las luces
    private bool lightState = false;
    public void Interact()
    {
        if(!lightState){
            lightState = true;
        } else {
            lightState = false;
        }
        OnClickSwitch?.Invoke(this, new OnClickSwitchEventArgs{lightState = lightState});
    }
}
