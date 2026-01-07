using System;
using System.Collections;
using UnityEngine;
using UnityEngine.ProBuilder.Shapes;
using UnityEngine.SceneManagement;

public class EnterGameAnimation : MonoBehaviour
{
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject camera3;
    [SerializeField] private GameObject door;

    private void MainMenuButtons_OnButtonPlayClicked(object sender, System.EventArgs e){
        StartCoroutine(CameraAnimation());
    }

    private void Start(){
        MainMenuButtons.Instance.OnButtonPlayedClicked += MainMenuButtons_OnButtonPlayClicked;
    }

    IEnumerator CameraAnimation(){
        // Ocultar el cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Pasa a camara 2
        camera1.SetActive(false);
        yield return new WaitForSecondsRealtime(3.0f);

        // Animacion puerta
        DoorHandler doorHandler = door.GetComponent<DoorHandler>();
        doorHandler.Interact();
        yield return new WaitForSecondsRealtime(1.0f);

        // Pasa a camara 3
        camera2.SetActive(false);
        yield return new WaitForSecondsRealtime(2.0f);
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(1.0f);

        // Carga escena nueva
        SceneManager.LoadScene(1);

        yield return null;
    }

    
}
