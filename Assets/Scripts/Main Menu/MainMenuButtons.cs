using System;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    public static MainMenuButtons Instance {get; private set;}
    public event EventHandler OnButtonPlayedClicked;
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void Awake(){
        Instance = this;

        playButton.onClick.AddListener(() =>
        {
            OnButtonPlayedClicked?.Invoke(this, EventArgs.Empty);
        });

        optionsButton.onClick.AddListener(() =>
        {
            Debug.Log("Options");
        });

        exitButton.onClick.AddListener(() =>
        {
            Debug.Log("Exit");
            Application.Quit();
        });
    }
}
