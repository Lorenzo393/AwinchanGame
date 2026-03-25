using System;
using UnityEngine;

public class LastPiecePuzzle : MonoBehaviour, IInteractable
{
    public event EventHandler OnLastPieceClicked;
    [SerializeField] private AudioClip accessGrantedSound;
    [SerializeField] private AudioClip accessDeniedSound;
    public void Interact(){
        if(SecretManager.Instance.GetCanOpen()) {
            SoundManager.Instance.PlaySound(accessGrantedSound, transform.position);
            gameObject.layer = LayerMask.NameToLayer("Default");
            OnLastPieceClicked?.Invoke(this, EventArgs.Empty);
            Destroy(this);
        } else {
            SoundManager.Instance.PlaySound(accessDeniedSound, transform.position);
        }
    }
}
