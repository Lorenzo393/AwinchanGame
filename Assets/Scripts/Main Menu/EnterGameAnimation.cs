using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnterGameAnimation : MonoBehaviour
{
    [SerializeField] private GameObject camera1;
    [SerializeField] private GameObject camera2;
    [SerializeField] private GameObject camera3;
    [SerializeField] private Transform door;

    private void MainMenuButtons_OnButtonPlayClicked(object sender, System.EventArgs e){
        StartCoroutine(CameraAnimation());
    }

    private void Start(){
        MainMenuButtons.Instance.OnButtonPlayedClicked += MainMenuButtons_OnButtonPlayClicked;
    }

    IEnumerator CameraAnimation(){
        Debug.Log("Animation");
        SceneManager.LoadScene(1);
        yield return null;
    }
}
