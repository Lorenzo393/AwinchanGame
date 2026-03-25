using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class NavMeshScapeAnimation : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshPlayer;
    [SerializeField] private Transform initialPosition;
    [SerializeField] private Transform finalPosition;
    private bool isAnimation = false;
    private void MainDoorTriggerAnimation_OnMainDoorInteraction(object sender, System.EventArgs e){
        StartCoroutine(Animation());
    }
    private void Start(){
        navMeshPlayer.enabled = false;
        MainDoorTriggerAnimation.Instance.OnMainDoorInteraction += MainDoorTriggerAnimation_OnMainDoorInteraction;
    }
    private void Update(){
        if (isAnimation) navMeshPlayer.SetDestination(finalPosition.position);
    }

    private IEnumerator Animation(){
        AwinchanAI.Instance.DisabilityAwinchan();
        GameInput.Instance.BlockCameraInput();
        GameInput.Instance.BlockPlayerInput();

        StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2.0f);
        ShowHideHud.Instance.Hide();
        PlayerController.Instance.TeleportPlayer(initialPosition.position);
        navMeshPlayer.enabled = true;

        yield return new WaitForSecondsRealtime(2f);

        StartCoroutine(FadeAnimation.Instance.FadeOut());
        yield return new WaitForSecondsRealtime(2.0f);
        
        isAnimation = true;
        yield return new WaitForSecondsRealtime(5.0f);

        StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2.0f);

        GameInput.Instance.EnableCameraInput();
        GameInput.Instance.EnablePlayerInput();
        CursorLock.Instance.EnableCursor();
        SceneManager.LoadScene(0);

        yield return null;
    }

}
