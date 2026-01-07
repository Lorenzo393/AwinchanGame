using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingTransition : MonoBehaviour
{
    private void Awake(){
        StartCoroutine(LoadingScene());
    }

    IEnumerator LoadingScene(){
        yield return new WaitForSecondsRealtime(0.25f);
        SceneManager.LoadScene(2);
        yield return null;
    }
}
