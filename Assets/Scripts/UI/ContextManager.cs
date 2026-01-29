using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextManager : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    [SerializeField] private string showSituationText = "Tengo que encontrar la manera de entrar";
    public event EventHandler OnCloseContextManager;
    private float showingTime = 0.7f;
    private float displayTime = 1f;
    private float hidingTime = 0.7f;
    private RawImage thisImage;

    private void Awake(){
        thisImage = GetComponent<RawImage>();
    }

    private void Start(){
        GameInput.Instance.BlockPlayerInput();
        closeButton.onClick.AddListener(() =>
        {
            thisImage.enabled = false;
            closeButton.gameObject.SetActive(false);
            FadeAnimation.Instance.FadeOutInstant();
            GameInput.Instance.EnablePlayerInput();
            CursorLock.Instance.BlockCursor();
            StartCoroutine(ShowAndDestroy());
        });
    }

    IEnumerator ShowAndDestroy(){
        SituationTextUI.Instance.ShowText(showSituationText, showingTime, displayTime, hidingTime);
        yield return new WaitForSecondsRealtime(showingTime + displayTime + hidingTime);
        OnCloseContextManager?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSecondsRealtime(2.0f);
        Destroy(this.gameObject);
    }
}
