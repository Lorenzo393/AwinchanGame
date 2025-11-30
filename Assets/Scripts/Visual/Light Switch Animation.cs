using UnityEngine;

public class LightSwitchAnimation : MonoBehaviour
{
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
            lightSwitch.rotation = Quaternion.Euler(0,90,0);
        }
        if(lightState == true){
            lightSwitch.rotation = Quaternion.Euler(15,90,0);
        }
    }

}
