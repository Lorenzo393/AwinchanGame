using System.Collections;
using UnityEngine;

public class EnterGameAnimation : MonoBehaviour
{
    [SerializeField] private Camera camera1;
    [SerializeField] private Camera camera2;
    [SerializeField] private Camera camera3;
    [SerializeField] private Transform door;

    private void Start(){
        StartCoroutine(cameraAnimation());
    }

    IEnumerator cameraAnimation(){
        yield return null;
    }
}
