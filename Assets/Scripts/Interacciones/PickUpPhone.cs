using System;
using UnityEngine;

public class PickUpPhone : MonoBehaviour, IInteractable
{
    public event EventHandler OnPickUpPhone;
    public void Interact()
    {
        OnPickUpPhone?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
