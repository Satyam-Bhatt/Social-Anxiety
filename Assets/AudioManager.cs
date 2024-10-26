using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;
    public static AudioManager Instance
    {
        get
        {
            _instance = FindObjectOfType<AudioManager>();
            if (!_instance)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }

            return _instance;
        }
    }

    public AudioSource audioSource1;
    public AudioSource audioSource2;

    public AudioClip coffeeGame_Audio;
    public AudioClip afterBW_Clip;
    public AudioClip beforeBW_Clip;
    public AudioClip breathing_Easy;
    public AudioClip breathing_Heavy;

    
    private void Awake()
    {
        //audioSource = GetComponent<AudioSource>();
    }

    public void AudioPlay(AudioClip clip)
    {
        audioSource1.Stop();
        audioSource1.clip = clip;
        audioSource1.Play();
    }
    public void AudioPlay2(AudioClip clip)
    {
        audioSource2.Stop();
        audioSource2.clip = clip;
        audioSource2.Play();
    }
}
