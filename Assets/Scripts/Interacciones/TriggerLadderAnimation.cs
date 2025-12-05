using System;
using System.Collections;
using UnityEngine;

public class TriggerLadderAnimation : MonoBehaviour, IInteractable
{
    public void Interact(){
        StartCoroutine(FadeInOut());
    }

    IEnumerator FadeInOut(){
        yield return StartCoroutine(FadeAnimation.Instance.FadeIn());
        yield return new WaitForSecondsRealtime(2);
        yield return StartCoroutine(FadeAnimation.Instance.FadeOut());
    }
}
