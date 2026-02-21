using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger26 : MonoBehaviour
{
    public event EventHandler OnTrigger26Enter;
    public event EventHandler OnTrigger26Exit;
    private void Start(){
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other){
        OnTrigger26Enter?.Invoke(this, EventArgs.Empty);
    }
    private void OnTriggerExit(Collider other){
        OnTrigger26Exit?.Invoke(this, EventArgs.Empty);
    }
}
