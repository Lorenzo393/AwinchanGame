using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingTransition : MonoBehaviour
{
    [SerializeField] private int sceneIndex = 2;

    private void Start()
    {
        StartCoroutine(LoadGame());
    }

    private IEnumerator LoadGame()
    {
        // Comenzar la carga asíncrona
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneIndex);

        // Evitar que la escena se active automáticamente
        operation.allowSceneActivation = false;

        // Esperar hasta que esté preparada
        while (operation.progress < 0.9f)
        {
            yield return null;
        }

        // La escena está lista para activarse
        operation.allowSceneActivation = true;
    }
}