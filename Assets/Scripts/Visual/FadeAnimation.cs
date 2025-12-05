using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeAnimation : MonoBehaviour
{
    public static FadeAnimation Instance{get; private set;}
    [SerializeField] private CanvasGroup canvas;
    private float fadeSpeed = 2f;
    private void Awake(){
        Instance = this;
    }
    public IEnumerator FadeIn(){
        for(float t = 0f ; t <= 1.0f ; t += Time.deltaTime * fadeSpeed){
            canvas.alpha = t;
            yield return null;
        }
        canvas.alpha = 1.0f;
    }
    public IEnumerator FadeOut(){
        for(float t = 1.0f ; t >= 0f ; t -= Time.deltaTime * fadeSpeed){
            canvas.alpha = t;
            yield return null;
        }
        canvas.alpha = 0f;
    }
}
