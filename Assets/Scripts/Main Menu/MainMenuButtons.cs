using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public static MainMenuButtons Instance {get; private set;}
    public event EventHandler OnButtonPlayedClicked;
    [SerializeField] private GameObject optionsCamera;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private Button returnButton;

    private void Awake(){
        Instance = this;

        playButton.onClick.AddListener(() =>
        {
            OnButtonPlayedClicked?.Invoke(this, EventArgs.Empty);
        });

        optionsButton.onClick.AddListener(() =>
        {
            optionsCamera.SetActive(true);
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
        
        returnButton.onClick.AddListener(() =>
        {
            optionsCamera.SetActive(false);
        });
    }
}
