using UnityEngine;

public class GlobalLightManager : MonoBehaviour, IInteractable
{
    // Referencia a la luna
    [SerializeField] private GameObject globalLight;
    // Estado de la luz
    private bool activeLight;
    private void Start(){
        activeLight = globalLight.activeSelf;
    }
    public void Interact(){
        activeLight = !activeLight;
        globalLight.SetActive(activeLight);
    }
}
