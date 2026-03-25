using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScapeAnimation : MonoBehaviour
{
    [SerializeField] private GameObject initialCamera;
    [SerializeField] private GameObject middleCamera;
    [SerializeField] private GameObject finalCamera;
    [SerializeField] private GameObject playerCamera;

    private void MainDoorTriggerAnimation_OnMainDoorInteraction(object sender, System.EventArgs e){
        StartCoroutine(Animation());
    }
    private void Start(){
        MainDoorTriggerAnimation.Instance.OnMainDoorInteraction += MainDoorTriggerAnimation_OnMainDoorInteraction;
    }

    private IEnumerator Animation(){
        AwinchanAI.Instance.DisabilityAwinchan();
        GameInput.Instance.BlockCameraInput();
        GameInput.Instance.BlockPlayerInput();

        StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2.0f);
        ShowHideHud.Instance.Hide();
        initialCamera.SetActive(true);
        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(FadeAnimation.Instance.FadeOut());
        yield return new WaitForSecondsRealtime(2.0f);
        middleCamera.SetActive(true);
        initialCamera.SetActive(false);

        yield return new WaitForSecondsRealtime(2.0f);
        finalCamera.SetActive(true);
        initialCamera.SetActive(false);


        yield return new WaitForSecondsRealtime(2.0f);
        StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2.0f);


        GameInput.Instance.EnableCameraInput();
        GameInput.Instance.EnablePlayerInput();
        CursorLock.Instance.EnableCursor();
        SceneManager.LoadScene(0);

        yield return null;
    }
}
