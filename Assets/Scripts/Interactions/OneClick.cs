using UnityEngine;

public class OneClick : MonoBehaviour
{
    [SerializeField] private GameObject soundMachine;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private AudioClip shortcircuitSound;
    private void LightSwitchInteraction_OnClickSwitch(object sender, System.EventArgs e){
        gameObject.layer = layerMask;
        SoundManager.Instance.PlaySound(shortcircuitSound, transform.position);
        Destroy(soundMachine);
    }
    private void Start(){
        LightSwitchInteraction lightSwitchInteraction = GetComponent<LightSwitchInteraction>();
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;
    }
}
