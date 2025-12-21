using System.Collections.Generic;
using UnityEngine;

public class LightsHandler : MonoBehaviour
{
    // Referencia al switch que prende y apaga las luces
    [SerializeField] private LightSwitchInteraction lightSwitchInteraction;
    // Lista de las luces
    [SerializeField] private List<Transform> lightList = new List<Transform>();

    private void LightSwitchInteraction_OnClickSwitch(object sender, LightSwitchInteraction.OnClickSwitchEventArgs e){
        LightsManager(e.lightState);
    }
    private void Awake(){
        // Evento que se dispara cuando se interactua con el switch de la luz
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
    private void LightsManager(bool lightState){
        if (lightState){
            foreach(Transform light in lightList){
                EnableEmission(light);
                EnableDisableLight(light, lightState);
            }
        } else {
            foreach(Transform light in lightList){
               DisableEmission(light); 
               EnableDisableLight(light, lightState);
            }
        }
    }
    private void EnableEmission(Transform obj){
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        mesh.material.EnableKeyword("_EMISSION");
    }
    private void DisableEmission(Transform obj){
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        mesh.material.DisableKeyword("_EMISSION");
    }
    private void EnableDisableLight(Transform obj, bool lightState){
        int childPosition = 0;
        Transform light = obj.GetChild(childPosition);
        light.gameObject.SetActive(lightState);
    }
    public void DisableLight(){
        foreach (Transform light in lightList){
            MeshRenderer mesh = light.GetComponent<MeshRenderer>();
            int childPosition = 0;

            mesh.material.DisableKeyword("_EMISSION");
            Transform lightSpot = light.GetChild(childPosition);
            lightSpot.gameObject.SetActive(false);

            // Me desubscribo del evento asi no se buggea
            lightSwitchInteraction.OnClickSwitch -= LightSwitchInteraction_OnClickSwitch;
        }
    }
}
