using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private PlayerAudioGenerator audioGenerator;
    public GameObject backgroundSounds;
    private AudioSource[] audioSources;

    void Start()
    {
        audioGenerator = GameObject.FindWithTag("Player").GetComponent<PlayerAudioGenerator>();
        audioSources = backgroundSounds.GetComponentsInChildren<AudioSource>();
    }

    void Update()
    {
        
    }

    public void SetSFXVolume(float volume)
    {
        if(audioGenerator)
        {
            audioGenerator.SetVolume(volume);
        }
    }

    public void SetBackgroundVolume(float volume)
    {
        if(audioSources.Length != 0)
        {
            foreach (AudioSource audioSource in audioSources)
            {
                audioSource.volume = volume;
            }
        }
    }
}
