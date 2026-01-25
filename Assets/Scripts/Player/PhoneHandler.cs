using UnityEngine;

public class PhoneHandler : MonoBehaviour
{
    // Sonido sacar guardar telefono
    [SerializeField] private AudioClip phoneSound; 
    // Referencia al telefono del jugador
    [SerializeField] private GameObject playerPhone;
    // Referencia al telefono a agarrar
    [SerializeField] private PickUpPhone pickUpPhone;


    // No agarrado/agarrado del telefono
    private bool phoneIsPickedUp = false;
    // Apagodo/encendido de la linterna
    private bool phoneLight = false;

    private void PickUpPhone_OnPickUpPhone(object sender, System.EventArgs e){
        phoneIsPickedUp = true;
    }
    private void GameInput_OnFlashlightAction(object sender, System.EventArgs e){
        if (phoneIsPickedUp){
            phoneLight = !phoneLight;
            playerPhone.SetActive(phoneLight);
            SoundManager.Instance.PlaySound(phoneSound, transform.position, 0.5f);
        }
    }

    private void Start()
    {
        // Evento encendido/apagado de linterna
        GameInput.Instance.OnFlashlightAction += GameInput_OnFlashlightAction;
        // Evento agarrar telefono
        pickUpPhone.OnPickUpPhone += PickUpPhone_OnPickUpPhone;

        // Estado inicial del telefono
        playerPhone.SetActive(phoneLight);
    }
}
