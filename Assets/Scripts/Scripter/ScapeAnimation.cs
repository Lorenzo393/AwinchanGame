using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScapeAnimation : MonoBehaviour
{
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
        yield return new WaitForSecondsRealtime(3.0f);

        GameInput.Instance.EnableCameraInput();
        GameInput.Instance.EnablePlayerInput();
        CursorLock.Instance.EnableCursor();
        SceneManager.LoadScene(0);

        yield return null;
    }
}
