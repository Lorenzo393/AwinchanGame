using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    public static FadeAnimation Instance{get; private set;}
    [SerializeField] private CanvasGroup canvas;
    private void Awake(){
        Instance = this;
    }
    public IEnumerator FadeIn(){
        canvas.alpha = 1f;
        yield return null;
    }
    public IEnumerator FadeOut(){
        canvas.alpha = 0f;
        yield return null;
    }
}
