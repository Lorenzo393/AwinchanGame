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
    [SerializeField] private GameObject deskLamp2;

    [Header ("Triggers")]
    [SerializeField] private GameObject chasingTrigger;
    [SerializeField] private GameObject stopTrigger;

    [Header ("Audios")]
    [SerializeField] private AudioSource fridgeSound;
    [SerializeField] private AudioSource janitorsMachineSound;
    [SerializeField] private AudioSource bathroomMachineSound;
    [SerializeField] private AudioSource thunderSound;

    [Header ("Situation text")]
    [SerializeField] private string situationText = "...";
    [SerializeField] private float showingTime = 0.5f;
    [SerializeField] private float displayTime = 0.9f;
    [SerializeField] private float hidingTime = 0.6f;

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

        // Espero 1 segundo
        yield return new WaitForSecondsRealtime(1.0f);

        // Trueno
        thunderSound.enabled = true;

        // Espero 1 segundo
        yield return new WaitForSecondsRealtime(1.0f);

        // Corte de luz
        foreach(LightsHandler lightsHandler in lightsHandlersList){
            lightsHandler.DisableLight();
            Destroy(lightsHandler);
        }
        foreach(GameObject light in lightsList) Destroy(light);

        DestroyLampEmition(deskLamp);
        DestroyLampEmition(deskLamp2);

        fridgeSound.enabled = false;
        janitorsMachineSound.enabled = false;
        bathroomMachineSound.enabled = false;


        // Espero 2 segundos
        yield return new WaitForSeconds(2);
        SituationTextUI.Instance.ShowText(situationText, showingTime, displayTime, hidingTime);
        thunderSound.volume = 0.95f;

        // Lluvia
        
        
        // Espero 2 segundos
        yield return new WaitForSecondsRealtime(2);
        thunderSound.volume = 0.90f;

        // Sonido metalico

        principalsKey.SetActive(true);

        // Destruir el objeto thunder
        yield return new WaitForSecondsRealtime(15);
        thunderSound.volume = 0.80f;
        Destroy(thunderSound.gameObject);

        yield return null;
    }

    private void DestroyLampEmition(GameObject obj){
        MeshRenderer mesh = obj.GetComponent<MeshRenderer>();
        mesh.material.DisableKeyword("_EMISSION");
    }
}
