using System.Collections;
using UnityEngine;

public class ElectricPanelAnimation : MonoBehaviour
{
    [SerializeField] private LightSwitchInteraction lightSwitchInteraction;
    private Quaternion initialRotation;
    private Quaternion finalRotation;
    private float rotationAngle = 135f;
    private float animationSpeed = 4f;
    private bool isMoving;

    private void LightSwitchInteraction_OnClickSwitch(object sender, LightSwitchInteraction.OnClickSwitchEventArgs e){
        if (!isMoving){
            if (e.lightState) StartCoroutine(UpDownSwitch(initialRotation, finalRotation));
            else StartCoroutine(UpDownSwitch(finalRotation, initialRotation));
        }
    }
    private void Start(){
        isMoving = false;
        lightSwitchInteraction.OnClickSwitch += LightSwitchInteraction_OnClickSwitch;

        initialRotation = transform.localRotation;
        finalRotation = Quaternion.Euler(-97f ,transform.localRotation.y,transform.localRotation.z);
    }

     IEnumerator UpDownSwitch(Quaternion initialRotation, Quaternion finalRotation){
        isMoving = true;
        for(float f = 0f ; f < 1.0f ; f += Time.deltaTime * animationSpeed){
            transform.localRotation = Quaternion.Slerp(initialRotation, finalRotation , f);
            yield return null;
        }
        isMoving = false;
    }
}
