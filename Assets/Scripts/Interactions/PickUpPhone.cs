using System;
using UnityEngine;

public class PickUpPhone : MonoBehaviour, IInteractable
{
    public static PickUpPhone Instance {get; private set;}
    public event EventHandler OnPickUpPhone;
    [SerializeField] private AudioClip pickUpPhoneSound;
    [SerializeField] private string situationText = "Ahora me voy de aqui";
    private float showingTime = 0.7f;
    private float displayTime = 1f;
    private float hidingTime = 0.7f;

    private void Awake(){
        Instance = this;
    }
    public void Interact()
    {
        SoundManager.Instance.PlaySound(pickUpPhoneSound, transform.position);
        OnPickUpPhone?.Invoke(this, EventArgs.Empty);
        SituationTextUI.Instance.ShowText(situationText, showingTime, displayTime, hidingTime);
        Destroy(gameObject);
    }
}
