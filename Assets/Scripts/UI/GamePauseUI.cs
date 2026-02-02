using System;
using System.Collections.Generic;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    public static GamePauseUI Instance {get; private set;}
    [SerializeField] private CinemachineInputAxisController axisController;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Toggle invertAxesToggle;
    [SerializeField] TextMeshProUGUI sensibilityShowerText;
    [SerializeField] private Slider sensSlider;
    private int invertAxis = -1;
    public event EventHandler OnBackToPlay;

    private void PauseManager_OnGamePaused(object sender, System.EventArgs e){
        Show();
    }
    private void PauseManager_OnGameUnpaused(object sender, System.EventArgs e){
        Hide();
    }
    private void Awake(){
        Instance = this;

        sensSlider.value = 1.0f;
        
        invertAxesToggle.onValueChanged.AddListener(isInverted =>
        {
            if (isInverted)invertAxis = 1; 
            else invertAxis = -1;
            
            foreach(var controller in axisController.Controllers){
                if (controller.Name == "Look X (Pan)")controller.Input.Gain = sensSlider.value;
                if (controller.Name == "Look Y (Tilt)") controller.Input.Gain = invertAxis * sensSlider.value;
            }
            Debug.Log(invertAxis);
        });

        resumeButton.onClick.AddListener(() =>
        {
            Hide();
            OnBackToPlay?.Invoke(this, EventArgs.Empty);
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
        });

        sensSlider.onValueChanged.AddListener(sens =>
        {
            foreach(var controller in axisController.Controllers){
                if (controller.Name == "Look X (Pan)"){
                    controller.Input.Gain = sens;
                    sensibilityShowerText.text = sens.ToString("F2");
                }

                if (controller.Name == "Look Y (Tilt)") {
                    controller.Input.Gain = invertAxis * sens;
                    sensibilityShowerText.text = sens.ToString("F2");
                }
            }
        });
    }
    private void Start(){
        PauseManager.Instance.OnGamePaused += PauseManager_OnGamePaused;
        PauseManager.Instance.OnGameUnpaused += PauseManager_OnGameUnpaused;
        Hide();
    }
    private void Show(){
        gameObject.SetActive(true);
    }
    private void Hide(){
        gameObject.SetActive(false);
    }
}
