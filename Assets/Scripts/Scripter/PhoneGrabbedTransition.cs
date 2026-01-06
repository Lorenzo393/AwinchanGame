using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGrabbedTransition : MonoBehaviour
{
    [SerializeField] private GameObject ladderFirstPosition;
    [SerializeField] private GameObject ladderFinalPosition;
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
        Destroy(ladderFirstPosition);
        ladderFinalPosition.SetActive(true);

        // Espero 2 segundos
        yield return new WaitForSecondsRealtime(2.0f);

        // Trueno

        // Corte de luz
        foreach(LightsHandler lightsHandler in lightsHandlersList){
            lightsHandler.DisableLight();
            Destroy(lightsHandler);
        }
        foreach(GameObject light in lightsList) Destroy(light);
        // Espero 2 segundos
        yield return new WaitForSeconds(2);

        // Lluvia
        
        
        // Espero 2 segundos
        yield return new WaitForSecondsRealtime(2);

        // Sonido metalico

        yield return null;
    }
}
