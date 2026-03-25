using UnityEngine;

public class HudInteractor : MonoBehaviour, IInteractable
{
    private bool isHidden = false;
    public void Interact(){
        isHidden = !isHidden;
        if (isHidden) ShowHideHud.Instance.Hide();
        else ShowHideHud.Instance.Show();
    }
}
