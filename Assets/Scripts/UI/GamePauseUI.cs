using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void PauseManager_OnGamePaused(object sender, System.EventArgs e){
        Show();
    }
    private void PauseManager_OnGameUnpaused(object sender, System.EventArgs e){
        Hide();
    }
    private void Awake(){
        resumeButton.onClick.AddListener(() =>
        {
            
        });

        mainMenuButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(0);
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
