using System;
using UnityEngine;

public class PickUpPhone : MonoBehaviour, IInteractable
{
    public static PickUpPhone Instance {get; private set;}
    public event EventHandler OnPickUpPhone;
    [SerializeField] private AudioClip pickUpPhoneSound;

    private void Awake(){
        Instance = this;
    }
    public void Interact()
    {
        SoundManager.Instance.PlaySound(pickUpPhoneSound, transform.position);
        OnPickUpPhone?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
