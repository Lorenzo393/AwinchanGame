using System.Collections;
using TMPro;
using UnityEngine;

public class SituationTextUI : MonoBehaviour
{
    public static SituationTextUI Instance{get; private set;}
    [SerializeField] private TextMeshProUGUI text;
    private CanvasGroup panel;

    private void Awake(){
        Instance = this;
    }
    private void Start(){
        panel = GetComponent<CanvasGroup>();
        panel.alpha = 0;
        text.alpha = 0;
    }

    public void ShowText(string newText, float showingTime = 1.5f, float displayTime = 3f, float hidingTime = 1.5f){
        text.text = newText;
        StartCoroutine(Show(showingTime));
        StartCoroutine(Wait(displayTime));
        StartCoroutine(Hide(hidingTime));
        //text.text = "";
    }
    IEnumerator Show(float showingTime){
        float timer = 0;
        for(float t = 0 ; t <= 1 ; t = timer / showingTime){
            panel.alpha = t;
            text.alpha = t;
            timer += Time.deltaTime;
            yield return null;
        }
        panel.alpha = 1;
        text.alpha = 1;
        yield return null;
    }

    IEnumerator Wait(float displayTime){
        yield return new WaitForSecondsRealtime(displayTime);
        yield return null;
    }
    IEnumerator Hide(float hidingTime){
        yield return new WaitForSecondsRealtime(hidingTime);
        panel.alpha = 0;
        text.alpha = 0;
        yield return null;
    }
    

}
