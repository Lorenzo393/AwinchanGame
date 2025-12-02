using UnityEngine;

public class LightSwitchAnimation : MonoBehaviour
{
    // Corregir hardcodeo de la rotacion de la tecla

    // Referencia a la manijita del switch
    [SerializeField] private Transform lightSwitch;

    private void LightSwitchInteraction_OnClickSwitch(object sender, LightSwitchInteraction.OnClickSwitchEventArgs e){
        Animation(e.lightState);
    }
    private void Start(){
        LightSwitchInteraction lightSwitchInteraction = GetComponent<LightSwitchInteraction>();
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
    private void Animation(bool lightState){
        if(lightState == false){
            lightSwitch.localRotation = Quaternion.Euler(0, 0, 0);
        }
        if(lightState == true){
            lightSwitch.localRotation = Quaternion.Euler(15, 0, 0);
        }
    }

}
