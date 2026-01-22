using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
public class CanInteractUI : MonoBehaviour
{
    private RectTransform canvasRect;
    private UnityEngine.UI.Image crosshairImage;
    [SerializeField] private Sprite normalCroshair;
    [SerializeField] private Sprite interactuableCroshair;
    private void PlayerInteract_OnCanInteract(object sender, System.EventArgs e){
        canvasRect.sizeDelta = new Vector2 (35f, 35f);
        crosshairImage.sprite = interactuableCroshair;
    }
    private void PlayerInteract_OnCantInteract(object sender, System.EventArgs e){
        canvasRect.sizeDelta = new Vector2 (13f, 13f);
        crosshairImage.sprite = normalCroshair;
    }
    private void Start(){
        PlayerInteract.Instance.OnCanInteract += PlayerInteract_OnCanInteract;
        PlayerInteract.Instance.OnCantInteract += PlayerInteract_OnCantInteract;

        crosshairImage = GetComponent<UnityEngine.UI.Image>();
        canvasRect = GetComponent<RectTransform>();

        crosshairImage.sprite = normalCroshair;
        canvasRect.sizeDelta = new Vector2 (12f, 12f);
    }
}
