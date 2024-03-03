using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public  class AudioManager : Singleton<AudioManager>
{
    public AudioClip[] soundEffects;
    public AudioSource backgroundMusicSource;
    public AudioSource sfxSource;// AudioSource specifically for background music
    bool isPlaying;
    private void Awake()
    {
        keepAlive = false;
    }

    public void PlaySoundWait(string soundName, float volume, float waitDelay = 0f)
    {
        StartCoroutine(PlaySound(soundName, volume, waitDelay));

    }

    public IEnumerator PlaySound(string soundName, float volume = 1f, float waitDelay = 0f)
    {
        yield return new WaitForSeconds(waitDelay);
        AudioClip clip = FindAudioClip(soundName);
        PlaySoundImmediate(soundName, volume);
    }

    //other sfx
    public void PlaySoundImmediate(string soundName, float volume = 1f)
    {
        AudioClip sfx = FindAudioClip(soundName);

        if (sfx != null)
        {
            AudioSource.PlayClipAtPoint(sfx, Camera.main.transform.position, volume);

        }
        else
        {
            Debug.LogWarning("sfx not found: " + soundName);
        }
    }

    //for footsteps and shooting
    public void PlaySoundPersistent(string soundName, float volume = 1f)
    {
        AudioClip sfx = FindAudioClip(soundName);

        if (sfxSource != null)
        {
            sfxSource.clip = sfx;
            sfxSource.volume = volume;
            sfxSource.Play();
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
