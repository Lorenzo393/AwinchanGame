using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGrabbedTransition : MonoBehaviour
{
    [SerializeField] private List<LightsHandler> lightsHandlersList;
    [SerializeField] private List<GameObject> lightsList;
    [SerializeField] private PickUpPhone pickUpPhone;

    private void PickUpPhone_OnPickUpPhone(object sender, System.EventArgs e){
        StartCoroutine(PhoneTransition());
    }
    private void Start(){
        pickUpPhone.OnPickUpPhone += PickUpPhone_OnPickUpPhone;
    }

    IEnumerator PhoneTransition(){
        foreach(LightsHandler lightsHandler in lightsHandlersList){
            lightsHandler.DisableLight();
            Destroy(lightsHandler);
        }
        foreach(GameObject light in lightsList){
            Destroy(light);
        }

        yield return null;
    }
}
