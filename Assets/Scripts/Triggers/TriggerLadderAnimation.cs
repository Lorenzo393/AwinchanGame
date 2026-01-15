using System;
using System.Collections;
using UnityEngine;

public class TriggerLadderAnimation : MonoBehaviour, IInteractable
{
    // Evento interaccion con la escalera
    public event EventHandler OnLadderInteraction;
    public void Interact(){
        OnLadderInteraction?.Invoke(this, EventArgs.Empty);
    }
}
