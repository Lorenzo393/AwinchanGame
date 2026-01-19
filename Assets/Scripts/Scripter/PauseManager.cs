using System;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance {get; private set;}
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    [SerializeField] private bool isPaused;
    private void GameInput_OnPauseAction(object sender, System.EventArgs e){
        isPaused = !isPaused;

        if (isPaused){
            GameInput.Instance.BlockCameraInput();
            CursorLock.Instance.EnableCursor();
            OnGamePaused?.Invoke(this,EventArgs.Empty);
            Time.timeScale = 0f;
        } else {
            Time.timeScale = 1f;
            CursorLock.Instance.BlockCursor();
            GameInput.Instance.EnableCameraInput();
            OnGameUnpaused?.Invoke(this,EventArgs.Empty);
        }
    }

    private void Awake(){
        Instance = this;
    }
    private void Start(){
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }
}
