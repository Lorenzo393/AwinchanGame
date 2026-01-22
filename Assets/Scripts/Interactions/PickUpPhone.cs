using System;
using UnityEngine;

public class PickUpPhone : MonoBehaviour, IInteractable
{
    public static PickUpPhone Instance {get; private set;}
    public event EventHandler OnPickUpPhone;

    private void Awake(){
        Instance = this;
    }
    public void Interact()
    {
        OnPickUpPhone?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
