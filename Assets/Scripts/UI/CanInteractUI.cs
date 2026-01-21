using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine.UI;
using UnityEngine;
public class CanInteractUI : MonoBehaviour
{
    private UnityEngine.UI.Image crosshairImage;
    [SerializeField] private Sprite normalCroshair;
    [SerializeField] private Sprite interactuableCroshair;
    private void PlayerInteract_OnCanInteract(object sender, System.EventArgs e){
        crosshairImage.sprite = interactuableCroshair;
    }
    private void PlayerInteract_OnCantInteract(object sender, System.EventArgs e){
        crosshairImage.sprite = normalCroshair;
    }
    private void Start(){
        PlayerInteract.Instance.OnCanInteract += PlayerInteract_OnCanInteract;
        PlayerInteract.Instance.OnCantInteract += PlayerInteract_OnCantInteract;

        crosshairImage = GetComponent<UnityEngine.UI.Image>();

        crosshairImage.sprite = normalCroshair;
    }
}
