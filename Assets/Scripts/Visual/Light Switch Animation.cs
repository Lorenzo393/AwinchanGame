using UnityEngine;

public class LightSwitchAnimation : MonoBehaviour
{
    // Referencia a la manijita del switch
    [SerializeField] private Transform lightSwitch;
    // Referencia al evento de interaccion con el switch
    private LightSwitchInteraction lightSwitchInteraction;
    // Encendido/apagado de las luces
    private bool lightState = false;


    private void LightSwitchInteraction_OnClickSwitch(object sender, System.EventArgs e){
        if(lightState == false){
            lightState = true;
        } else if (lightState == true){
            lightState = false;
        }
        Animation(lightState);
    }
    private void Awake(){
        lightSwitchInteraction = GetComponent<LightSwitchInteraction>();
    }
    private void Start(){
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
    private void Animation(bool lightState){
        if(lightState == false){
            lightSwitch.rotation = Quaternion.Euler(0,90,0);
        }
        if(lightState == true){
            lightSwitch.rotation = Quaternion.Euler(15,90,0);
        }
    }

}
