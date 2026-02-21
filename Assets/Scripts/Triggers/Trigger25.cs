using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Trigger25 : MonoBehaviour
{
    public event EventHandler OnTrigger25Enter;
    public event EventHandler OnTrigger25Exit;
    private void Start(){
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other){
        OnTrigger25Enter?.Invoke(this, EventArgs.Empty);
    }
    private void OnTriggerExit(Collider other){
        OnTrigger25Exit?.Invoke(this, EventArgs.Empty);
    }
}
