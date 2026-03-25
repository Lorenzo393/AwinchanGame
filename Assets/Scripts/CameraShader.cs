using UnityEngine;

public class CameraShader : MonoBehaviour
{
    [SerializeField] private Shader unlitShader;
    private void Start(){
        GetComponent<Camera>().SetReplacementShader(unlitShader, "");
    }
}
