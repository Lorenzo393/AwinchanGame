using System.Collections;
using UnityEngine;

public class LadderAnimation : MonoBehaviour
{
    // Referencia a la escalera del piso
    [SerializeField] private Transform ladderFloor;
    // Referencia a la escalera de la pared
    [SerializeField] private GameObject ladderWall;
    // Referencia a la posicion final del jugador
    [SerializeField] private Transform playerNewTransform;

    private void TriggerLadderAnimation_OnLadderInteraction(object sender, System.EventArgs e){
        StartCoroutine(FadeInOut());
    }
    private void Start(){
        // Subscripcion al evento trigger de la escalera
        TriggerLadderAnimation triggerLadderAnimation = ladderFloor.GetComponent<TriggerLadderAnimation>();
        triggerLadderAnimation.OnLadderInteraction += TriggerLadderAnimation_OnLadderInteraction;
        
    }
    IEnumerator FadeInOut(){
        // Fade In + Cambio de escaleras
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2);

        GameInput.Instance.BlockPlayerInput();
        GameInput.Instance.BlockCameraInput();
        PlayerController.Instance.TeleportPlayer(playerNewTransform.position);

        if(ladderFloor != null) {
            Destroy(ladderFloor.gameObject);
        }
        ladderWall.SetActive(true);

        yield return StartCoroutine(FadeAnimation.Instance.FadeOut());
        yield return new WaitForSecondsRealtime(1);
        GameInput.Instance.EnablePlayerInput();
        GameInput.Instance.EnableCameraInput();
    }
}