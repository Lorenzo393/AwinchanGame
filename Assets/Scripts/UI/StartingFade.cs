using System.Collections;
using UnityEngine;

public class StartingFade : MonoBehaviour
{
    private void Awake(){
        StartCoroutine(Fade());
    }

    IEnumerator Fade(){
        yield return StartCoroutine(FadeAnimation.Instance.FadeOut());
    }
}
