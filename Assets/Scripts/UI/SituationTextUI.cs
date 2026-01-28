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
    }

    public void ShowText(string newText, float showingTime = 1.5f, float displayTime = 3f, float hidingTime = 2f){
        text.text = newText;
        StartCoroutine(ShowWaitHide(showingTime, displayTime, hidingTime));
    }
    IEnumerator ShowWaitHide(float showingTime, float displayTime, float hidingTime){
        float timer = 0;
        for(float t = panel.alpha ; t <= 1f ; t = timer / showingTime){
            panel.alpha = t;
            timer += Time.deltaTime;
            yield return null;
        }
        panel.alpha = 1;

        StartCoroutine(Wait(displayTime, hidingTime));
    }

    IEnumerator Wait(float displayTime, float hidingTime){
        yield return new WaitForSecondsRealtime(displayTime);

        StartCoroutine(Hide(hidingTime));
    }
    IEnumerator Hide(float hidingTime){
        float timer = 0;
        for(float t = panel.alpha ; t >= 0f ; t = -((timer / hidingTime) - 1)){
            panel.alpha = t;
            timer += Time.deltaTime;
            yield return null;
        }
        panel.alpha = 0;
    }
    

}
