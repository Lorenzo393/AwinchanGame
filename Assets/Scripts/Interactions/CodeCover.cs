using UnityEngine;

public class CodeCover : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip destroySound;
    public void Interact(){
        SoundManager.Instance.PlaySound(destroySound, transform.position);
        Destroy(gameObject);
    }
}
