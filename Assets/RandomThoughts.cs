using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomThoughts : MonoBehaviour
{
    public AudioClip[] clips; 

    public void ClipPlay_Immediate(int clipIndex)
    {
        GameManager.Instance.AudioPlay(clips[clipIndex]);
    }

    public void ClipPlay_Delay(int clipIndex, float delay)
    { 
        StartCoroutine(DelayAudio(clipIndex, delay));
    }

    IEnumerator DelayAudio(int clipIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.AudioPlay(clips[clipIndex]);
    }
}
