using System.Numerics;
using UnityEngine;

public class playerController : MonoBehaviour
{
    private float h;
    private float v;
    private UnityEngine.Vector3 playerInput;
    public float speed;
    public CharacterController player;
    
    void Start(){
        player = GetComponent<CharacterController>();
    }
    void Update(){
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        playerInput = new UnityEngine.Vector3(h,0f,v);
        playerInput = UnityEngine.Vector3.ClampMagnitude(playerInput,1);

        player.Move(speed * Time.deltaTime * playerInput);
    }   
}