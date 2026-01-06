using UnityEngine;
using UnityEngine.UI;

public class MainMenuButtons : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button exitButton;

    private void Awake(){
        playButton.onClick.AddListener(() =>
        {
            Debug.Log("Play");
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
