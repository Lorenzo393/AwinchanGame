using System;
using UnityEngine;

public class Key : MonoBehaviour, IInteractable
{
    [SerializeField] private KeyTipe keyTipe;   
    public enum KeyTipe{
        Null,
        DosCinco,
        UnoCuatro,
        Process,
        Principals,
        Library,
        Outside,
    }
    public void Interact(){
        PlayerInventory.Instance.AddKey(keyTipe);
        Destroy(gameObject);
    }
}
