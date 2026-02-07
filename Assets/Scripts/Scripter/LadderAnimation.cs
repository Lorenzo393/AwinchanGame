using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LadderAnimation : MonoBehaviour
{
    public static LadderAnimation Instance {get; private set;}
    // Evento subir escalera
    public event EventHandler OnClimbLadder;
    // Referencia a la escalera del piso
    [SerializeField] private Transform ladderFloor;
    // Referencia a la escalera de la pared
    [SerializeField] private GameObject ladderWall;
    // Referencia a la camara 1
    [SerializeField] private GameObject camera1;
    // Referencia a la camara 2
    [SerializeField] private GameObject camera2;
    // Referencia a la ventana del conserje
    [SerializeField] private GameObject movingWindow;
    // Referencia a la posicion final del jugador
    [SerializeField] private Transform playerNewTransform;
    // Referencia al script de transicion de volumen
    [SerializeField] private VolumeTransition volumeTransition;
    // Referencia al sonido de la escalera
    [SerializeField] private AudioSource ladderClimbSound;
    

    private void TriggerLadderAnimation_OnLadderInteraction(object sender, System.EventArgs e){
        StartCoroutine(FadeInOut());
    }

    private void Awake(){
        Instance = this;
    }
    private void Start(){
        // Subscripcion al evento trigger de la escalera
        TriggerLadderAnimation triggerLadderAnimation = ladderFloor.GetComponent<TriggerLadderAnimation>();
        triggerLadderAnimation.OnLadderInteraction += TriggerLadderAnimation_OnLadderInteraction;
        
    }
    IEnumerator FadeInOut(){
        GameInput.Instance.BlockPlayerInput();
        GameInput.Instance.BlockCameraInput();
        
        // Fade In + Cambio de escaleras
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(1.5f);
        camera1.SetActive(true);

        // Borra una escalera y activa la otra
        if(ladderFloor != null) Destroy(ladderFloor.gameObject);
        ladderWall.SetActive(true);

        // Animacion de subir escalera
        ShowHideHud.Instance.Hide();
        OnClimbLadder?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSecondsRealtime(2f);
        yield return StartCoroutine(FadeAnimation.Instance.FadeOut());
        yield return new WaitForSecondsRealtime(1.0f);
        camera2.SetActive(true);
        ladderClimbSound.enabled = true;
        yield return new WaitForSecondsRealtime(3.0f);
        ladderClimbSound.enabled = false;
        Destroy(ladderClimbSound.gameObject);
        WindowAnimation windowAnimation = movingWindow.GetComponent<WindowAnimation>();
        windowAnimation.Interact();
        yield return new WaitForSecondsRealtime(1.0f);
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        ShowHideHud.Instance.Show();
        volumeTransition.SetVolume(0.009f);
        yield return new WaitForSecondsRealtime(2.0f);
        volumeTransition.SetVolume(0.003f);
        PlayerController.Instance.TeleportPlayer(playerNewTransform.position);
        if(camera1 != null) Destroy(camera1);
        if(camera2 != null) Destroy(camera2);
        yield return new WaitForSecondsRealtime(2.0f);

        // Volver al estado base
        yield return StartCoroutine(FadeAnimation.Instance.FadeOut());
        yield return new WaitForSecondsRealtime(1.0f);
        GameInput.Instance.EnablePlayerInput();
        GameInput.Instance.EnableCameraInput();
    }
}