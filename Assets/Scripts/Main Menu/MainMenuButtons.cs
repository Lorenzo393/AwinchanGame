using System;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private TMP_Dropdown resolutionsDropdown;
    [SerializeField] private Toggle fullscreenToggle;
    [SerializeField] private Slider volumeSlider;
    private Resolution[] resolutions;

    private void Awake(){
        Instance = this;

        Time.timeScale = 1f;

        ResolutionsInitialization();

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

        resolutionsDropdown.onValueChanged.AddListener( resolutionIndex =>
        {
            Resolution resolution = resolutions[resolutionIndex];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        });

        fullscreenToggle.onValueChanged.AddListener( isFullscreen =>
        {
            Screen.fullScreen = isFullscreen;
        });

        volumeSlider.onValueChanged.AddListener( volume =>
        {
            
        });
    }

    private void ResolutionsInitialization(){
        resolutions = Screen.resolutions;

        resolutionsDropdown.ClearOptions();
        
        List<String> resolutionsStrings = new List<string>();

        int numberOfResolutions = 0;
        int actualResolutionIndex = 0;

        foreach(Resolution res in resolutions){
            resolutionsStrings.Add(res.ToString());
            numberOfResolutions++;
            if(res.width == Screen.currentResolution.width && res.height == Screen.currentResolution.height){
                actualResolutionIndex = numberOfResolutions;
            }
        }

        resolutionsDropdown.AddOptions(resolutionsStrings);
        resolutionsDropdown.value = actualResolutionIndex;
        resolutionsDropdown.RefreshShownValue();
    }
}
