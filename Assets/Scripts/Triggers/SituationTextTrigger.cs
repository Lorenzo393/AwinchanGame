using UnityEngine;
using UnityEngine.Rendering;

public class SituationTextTrigger : MonoBehaviour
{
    [SerializeField] private AwinchanStopTrigger stopTrigger;
    [SerializeField] private string situationText = "Debo ir a la dirección, seguro que tiene un juego de llaves de repuesto en algún lado";
    private float showingTime = 0.7f;
    private float displayTime = 2.2f;
    private float hidingTime = 0.7f;
    private void StopTrigger_OnStopTriggerEnter(object sender, System.EventArgs e){
        gameObject.SetActive(true);
    }
    private void Awake(){
        gameObject.SetActive(false);
    }
    private void Start(){
        stopTrigger.OnStopTriggerEnter += StopTrigger_OnStopTriggerEnter;
    }
    
    private void OnTriggerEnter(Collider other){
        SituationTextUI.Instance.ShowText(situationText, showingTime, displayTime, hidingTime);
        Destroy(this.gameObject);
    }
}
