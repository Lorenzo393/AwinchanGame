using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingTransition : MonoBehaviour
{
    [SerializeField] private int sceneBuildIndex = 2;
    [SerializeField] private Slider progressBar;
    [SerializeField] private float minimumDisplayTime = 0.25f;

    private void Start(){
        StartCoroutine(LoadingScene());
    }

    private IEnumerator LoadingScene(){
        yield return new WaitForSecondsRealtime(minimumDisplayTime);

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneBuildIndex);

        if (operation == null){
            Debug.LogError("No se pudo iniciar la carga de la escena.");
            yield break;
        }

        while (!operation.isDone){
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null)
                progressBar.value = progress;

            yield return null;
        }
    }
}