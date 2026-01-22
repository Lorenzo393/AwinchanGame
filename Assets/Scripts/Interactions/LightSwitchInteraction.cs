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
    [SerializeField] private bool lightState;
    private void Start(){
        OnClickSwitch?.Invoke(this, new OnClickSwitchEventArgs{lightState = lightState});
    }
    public void Interact(){
        lightState = !lightState;
        OnClickSwitch?.Invoke(this, new OnClickSwitchEventArgs{lightState = lightState});
    }
}
