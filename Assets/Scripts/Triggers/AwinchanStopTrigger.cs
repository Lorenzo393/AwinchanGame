using System;
using UnityEngine;

public class AwinchanStopTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject newTrigger;
    public event EventHandler OnStopTriggerEnter;
    private void Start(){
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter(Collider other)
    {
        OnStopTriggerEnter?.Invoke(this, EventArgs.Empty);
        //newTrigger.SetActive(true);
        Destroy(this.gameObject);
    }
}
