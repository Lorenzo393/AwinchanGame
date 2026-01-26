using System;
using UnityEngine;

public class SecretPuzzlePiece : MonoBehaviour, IInteractable
{
    public event EventHandler OnPieceClicked;
    [SerializeField] private AudioClip interactSound;
    public void Interact(){
        SoundManager.Instance.PlaySound(interactSound, transform.position);
        gameObject.layer = LayerMask.NameToLayer("Default");
        OnPieceClicked?.Invoke(this, EventArgs.Empty);
        Destroy(this);
    }
}
