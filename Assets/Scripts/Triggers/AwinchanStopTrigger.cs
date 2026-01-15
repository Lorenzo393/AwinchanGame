using System;
using UnityEngine;

public class AwinchanStopTrigger : MonoBehaviour
{
    public event EventHandler OnStopTriggerEnter;
    private void Awake(){
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        OnStopTriggerEnter?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}
