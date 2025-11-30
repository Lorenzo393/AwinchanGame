using System.Collections.Generic;
using UnityEngine;

public class LightsHandler : MonoBehaviour
{
    // Referencia al switch que prende y apaga las luces
    [SerializeField] private LightSwitchInteraction lightSwitchInteraction;

    private void LightSwitchInteraction_OnClickSwitch(object sender, LightSwitchInteraction.OnClickSwitchEventArgs e){
        LightsManager(e.lightState);
    }

    private void Start(){
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }

    private void LightsManager(bool lightState){
        gameObject.SetActive(lightState);
    }
}
