using UnityEngine;

public class LightSwitchAnimation : MonoBehaviour
{
    // Corregir hardcodeo de la rotacion de la tecla

    // Referencia a la manijita del switch
    [SerializeField] private Transform lightSwitch;

    private void LightSwitchInteraction_OnClickSwitch(object sender, LightSwitchInteraction.OnClickSwitchEventArgs e){
        Animation(e.lightState);
    }
    private void Awake(){
        LightSwitchInteraction lightSwitchInteraction = GetComponent<LightSwitchInteraction>();
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
    private void Animation(bool lightState){
        if(!lightState) lightSwitch.localRotation = Quaternion.Euler(0, 0, 0);
        else lightSwitch.localRotation = Quaternion.Euler(15, 0, 0);
    }

}
