using UnityEngine;

public class MoonLightManager : MonoBehaviour, IInteractable
{
    // Referencia a la luna
    [SerializeField] private GameObject moonLight;
    // Estado de la luz
    private bool activeLight;

    private void Start(){
        activeLight = moonLight.activeSelf;
    }
    public void Interact(){
        activeLight = !activeLight;
        moonLight.SetActive(activeLight);
    }
}
