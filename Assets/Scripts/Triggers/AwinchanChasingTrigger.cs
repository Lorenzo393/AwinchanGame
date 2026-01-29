using System;
using UnityEngine;

public class AwinchanChasingTrigger : MonoBehaviour
{
    public event EventHandler OnChasingTriggerEnter;
    private void Start(){
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        OnChasingTriggerEnter?.Invoke(this, EventArgs.Empty);
        Destroy(this.gameObject);
    }
}
