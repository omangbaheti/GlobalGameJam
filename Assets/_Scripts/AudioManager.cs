using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioClip[] soundEffects;
    public AudioSource backgroundMusicSource;  // AudioSource specifically for background music


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(string soundName, float volume = 1f)
    {
        AudioClip clip = FindAudioClip(soundName);

        if (clip != null)
        {
            AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position, volume);
        }
        else
        {
            Debug.LogWarning("Audio clip not found: " + soundName);
        }
    }

    private AudioClip FindAudioClip(string soundName)
    {
        foreach (AudioClip clip in soundEffects)
        {
            if (clip.name == soundName)
            {
                return clip;
            }
        }
        return null;
    }

    public void playBkgd(string musicName, float volume = 1f){

        AudioClip clip = FindAudioClip(musicName);

        if (clip != null)
        {
            backgroundMusicSource.clip = clip;
            backgroundMusicSource.volume = volume;
            backgroundMusicSource.Play();
        }
        else
        {
            Debug.LogWarning("Background music not found: " + musicName);
        }
    }

}
