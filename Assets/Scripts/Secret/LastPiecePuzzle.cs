using System;
using UnityEngine;

public class LastPiecePuzzle : MonoBehaviour, IInteractable
{
    public event EventHandler OnLastPieceClicked;
    [SerializeField] private AudioClip interactSound;
    public void Interact(){
        if(SecretManager.Instance.GetCanOpen()) {
            SoundManager.Instance.PlaySound(interactSound, transform.position);
            gameObject.layer = LayerMask.NameToLayer("Default");
            OnLastPieceClicked?.Invoke(this, EventArgs.Empty);
            Destroy(this);
        } else {
            SoundManager.Instance.PlaySound(interactSound, transform.position);
        }
    }
}
