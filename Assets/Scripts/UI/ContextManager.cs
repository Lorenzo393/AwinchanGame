using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextManager : MonoBehaviour
{
    [SerializeField] private Button closeButton;
    private RawImage thisImage;

    private void Awake(){
        thisImage = GetComponent<RawImage>();
    }

    private void Start(){
        GameInput.Instance.BlockPlayerInput();
        closeButton.onClick.AddListener(() =>
        {
            thisImage.enabled = false;
            FadeAnimation.Instance.FadeOutInstant();
            GameInput.Instance.EnablePlayerInput();
            CursorLock.Instance.BlockCursor();
            Destroy(this.gameObject);
        });
    }
}
