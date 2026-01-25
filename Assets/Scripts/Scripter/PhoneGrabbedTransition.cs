using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneGrabbedTransition : MonoBehaviour
{
    public static PhoneGrabbedTransition Instance {get; private set;}

    [Header ("Ladders")]
    [SerializeField] private GameObject ladderFirstPosition;
    [SerializeField] private GameObject ladderFinalPosition;

    [Header ("Lights")]
    [SerializeField] private List<LightsHandler> lightsHandlersList;
    [SerializeField] private List<GameObject> lightsList;
    [SerializeField] private GameObject deskLamp;

    [Header ("Triggers")]
    [SerializeField] private GameObject chasingTrigger;
    [SerializeField] private GameObject stopTrigger;

    [Header ("Audios")]
    [SerializeField] private AudioSource fridgeSound;
    [SerializeField] private AudioSource janitorsMachineSound;
    [SerializeField] private AudioSource bathroomMachineSound;

    [Header ("Other things")]
    [SerializeField] private PickUpPhone pickUpPhone;
    [SerializeField] private GameObject principalsKey;
    

    private void PickUpPhone_OnPickUpPhone(object sender, System.EventArgs e){
        StartCoroutine(PhoneTransition());
    }
    private void Awake(){
        Instance = this;
    }
    private void Start(){
        PickUpPhone.Instance.OnPickUpPhone += PickUpPhone_OnPickUpPhone;
        principalsKey.SetActive(false);
    }

    IEnumerator PhoneTransition(){
        Destroy(ladderFirstPosition);
        ladderFinalPosition.SetActive(true);
        chasingTrigger.SetActive(true);
        stopTrigger.SetActive(true);

        // Espero 2 segundos
        yield return new WaitForSecondsRealtime(2.0f);

        // Trueno

        // Corte de luz
        foreach(LightsHandler lightsHandler in lightsHandlersList){
            lightsHandler.DisableLight();
            Destroy(lightsHandler);
        }
        foreach(GameObject light in lightsList) Destroy(light);

        DestroyLampEmition(deskLamp);

        fridgeSound.enabled = false;
        janitorsMachineSound.enabled = false;
        bathroomMachineSound.enabled = false;



        // Espero 2 segundos
        yield return new WaitForSeconds(2);

        // Lluvia
        
        
        // Espero 2 segundos
        yield return new WaitForSecondsRealtime(2);

        // Sonido metalico

        principalsKey.SetActive(true);
        yield return null;
    }

    private void DestroyLampEmition(GameObject obj){
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        mesh.material.DisableKeyword("_EMISSION");
    }
}
