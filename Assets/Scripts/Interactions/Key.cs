using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private AudioClip pickUpSound;
    [SerializeField] private KeyTipe keyTipe;   
    public enum KeyTipe{
        Null,
        UnoCuatro,
        DosCinco,
        DosSeis,
        Principals,
        Library,
        Outside,
    }
    public void Interact(){
        PlayerInventory.Instance.AddKey(keyTipe);
        SoundManager.Instance.PlaySound(pickUpSound,transform.position);
        Destroy(gameObject);
    }
}
