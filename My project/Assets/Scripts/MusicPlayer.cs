using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.loop = true; 
            if (!audioSource.isPlaying)
            {
                audioSource.Play(); 
            }
        }
        else
        {
            Debug.LogWarning("No AudioSource found on MusicPlayer GameObject.");
        }
    }

    public void SetVolume(float volume)
    {
        if (audioSource != null)
        {
            audioSource.volume = Mathf.Clamp01(volume); 
        }
    }

    public void StopMusic()
    {
        if (audioSource != null)
        {
            audioSource.Stop(); 
        }
    }
}
