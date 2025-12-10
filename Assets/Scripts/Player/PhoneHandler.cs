using UnityEngine;

public class PhoneHandler : MonoBehaviour
{
    /*
    // Creo que no es del todo correcto este enfoque pero como solo va a haber un objeto con el comportamiento de la linterna del telefono 
    // lo hago de esta manera.
    // Voy a tener que usar un enfoque diferente cuando tenga que programar que el personaje pueda agarrar llaves
    */
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
        if (phoneIsPickedUp)
        {
            phoneLight = !phoneLight;
            playerPhone.SetActive(phoneLight);
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
