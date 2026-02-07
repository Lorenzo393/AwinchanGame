using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ShowHideHud : MonoBehaviour
{
    [SerializeField] private List<GameObject> canvasObjects;
    public static ShowHideHud Instance {get; private set;}

    private void Start(){
        Instance = this;
    }

    public void Show(){
        foreach(GameObject canvas in canvasObjects){
            canvas.SetActive(true);
        }
    }
    public void Hide(){
        foreach(GameObject canvas in canvasObjects){
            canvas.SetActive(false);
        }
    }
}
