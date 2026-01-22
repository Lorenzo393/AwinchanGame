using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}

    private void Awake(){
        Instance = this;
    }   
    public void PlaySound(AudioClip clip, Vector3 position, float volume = 1f){
        AudioSource.PlayClipAtPoint(clip, position, volume);
    }
}
