using Unity.VisualScripting;
using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    [SerializeField] private GameObject telefono;
    [SerializeField] private Transform playerCameraTransform;
    [SerializeField] private LayerMask pickUpLayerMask;
    private bool linternaEncendida = false;
    private bool linternaObtenida = false;

    private void Update(){
        if(Input.GetMouseButtonDown(0)){ //Click izq
            if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward,out RaycastHit raycastHit,2.5f,pickUpLayerMask)){
                if(raycastHit.transform.TryGetComponent(out Telefono tel)){
                    tel.gameObject.SetActive(false);
                    linternaObtenida = true;
                }
            }
        }
        if(Input.GetMouseButtonDown(1) && linternaObtenida){ //Click der
            linternaEncendida = !linternaEncendida;
        }
        telefono.SetActive(linternaEncendida);
    }
}
