using UnityEngine;

public class playerInteractions : MonoBehaviour
{
    public GameObject bloque;
    public GameObject telefono;
    public Transform playerCameraTransform;
    public LayerMask pickUpLayerMask;
    private bool linternaEncendida;
    private bool linternaObtenida;

    void Start(){
        linternaEncendida = false;
        linternaObtenida = false;
    }

    void Update(){
        if(Input.GetMouseButtonDown(0)){ //Click izq
            if(Physics.Raycast(playerCameraTransform.position,playerCameraTransform.forward,out RaycastHit raycastHit,2.5f,pickUpLayerMask)){
                raycastHit.transform.gameObject.SetActive(false);
                linternaObtenida = true;
            }
        }
        if(Input.GetMouseButtonDown(1) && linternaObtenida){ //Click der
            linternaEncendida = !linternaEncendida;
        }
        telefono.SetActive(linternaEncendida);
    }
}
