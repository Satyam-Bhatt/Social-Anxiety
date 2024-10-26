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

    [SerializeField] private AudioSource audioSource1;
    [SerializeField] private AudioSource audioSource2;

    public AudioClip coffeeGame_Audio;
    public AudioClip afterBW_Clip;
    public AudioClip beforeBW_Clip;
    public AudioClip breathing;

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
}
