using System.Collections;
using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathAnimation : MonoBehaviour
{
    [SerializeField] private GameObject playerCamera;
    [SerializeField] private Transform awinchanFace;
    private void AwinchanAI_OnAwinchanAttack(object sender, System.EventArgs e){
        StartCoroutine(AwinchanAttack());
    }
    private void Start(){
        AwinchanAI.Instance.OnAwinchanAttack += AwinchanAI_OnAwinchanAttack;
    }

    IEnumerator AwinchanAttack(){

        CinemachineCamera playerVCam = playerCamera.GetComponent<CinemachineCamera>();
        GameInput.Instance.BlockCameraInput();
        GameInput.Instance.BlockPlayerInput();
        //playerCamera.Follow = awinchanFace;
        playerVCam.LookAt = awinchanFace;
        
        Destroy(playerCamera.GetComponent<CinemachinePanTilt>());
        playerVCam.AddComponent<CinemachineHardLookAt>();
        ShowHideHud.Instance.Hide();
        
        
        yield return new WaitForSecondsRealtime(1.3f);
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(1.8f);
        
        GoToMenu();
    }
    private void GoToMenu(){
        GameInput.Instance.EnableCameraInput();
        GameInput.Instance.EnablePlayerInput();
        CursorLock.Instance.EnableCursor();
        SceneManager.LoadScene(0);
    }
}
