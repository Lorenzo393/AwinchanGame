using UnityEngine;

public class OnOffScreen : MonoBehaviour
{
    [SerializeField] private LightSwitchInteraction lightSwitchInteraction;
    [SerializeField] private GameObject screen;
    private bool isActive = true;
    private void LightSwitchInteraction_OnClickSwitch(object sender, System.EventArgs e){
        isActive = !isActive;
        screen.SetActive(isActive);
    }
    private void Start(){
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
}
