using System.Collections;
using UnityEngine;

public class TriggerLadderAnimation : MonoBehaviour, IInteractable
{
    public void Interact(){
        StartCoroutine(Hola());
    }

    IEnumerator Hola(){
        for (int i = 0 ; i < 7 ; i++){
        Debug.Log("Paso: " + i);
        yield return new WaitForSecondsRealtime(1);
        }
    }
}
