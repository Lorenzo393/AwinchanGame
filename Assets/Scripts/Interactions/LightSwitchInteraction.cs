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
    [SerializeField] private AudioClip switchInteractionSound;
    // Encendido/apagado de las luces
    [SerializeField] private bool lightState;
    private void Start(){
        OnClickSwitch?.Invoke(this, new OnClickSwitchEventArgs{lightState = lightState});
    }
    public void Interact(){
        lightState = !lightState;
        SoundManager.Instance.PlaySound(switchInteractionSound, transform.position,0.8f);
        OnClickSwitch?.Invoke(this, new OnClickSwitchEventArgs{lightState = lightState});
    }
}
