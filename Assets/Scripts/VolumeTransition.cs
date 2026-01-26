using UnityEngine;
using UnityEngine.Rendering;

public class VolumeTransition : MonoBehaviour
{
    [SerializeField] private float volume = 0.04f;
    private AudioSource audioSource;
    private void Awake(){
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
    }
    public void SetVolume(float newVolume){
        volume = newVolume;
        audioSource.volume = volume;
    }
}
