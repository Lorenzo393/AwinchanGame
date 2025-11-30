using UnityEngine;

public class OnOffLights : MonoBehaviour
{
    // Referencia al switch que prende y apaga las luces
    [SerializeField] LightSwitchInteraction lightSwitchInteraction;
    // Encendido/Apagado de las luces
    private bool isOn = true;

    private void LightSwitchInteraction_OnClickSwitch(object sender, System.EventArgs e){
        if (isOn == true){
            isOn = false;
        } else if (isOn == false){
            isOn = true;
        }
        Debug.Log(isOn);
    }

    private void Start()
    {
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
}
