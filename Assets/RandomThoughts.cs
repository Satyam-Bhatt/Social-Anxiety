using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomThoughts : MonoBehaviour
{
    public AudioCaptionMix[] audioCaption;

    [SerializeField] private GameObject captionPanel;

    private float[] closeTime_Private;
    private AudioClip[] audioClips_Private;

    private void Start()
    {
        captionPanel.SetActive(false);
        closeTime_Private = new float[audioCaption.Length];
        audioClips_Private = new AudioClip[audioCaption.Length];

        for (int i = 0; i < audioCaption.Length; i++)
        { 
            closeTime_Private[i] = audioCaption[i].clip.length;
            audioClips_Private[i] = audioCaption[i].clip;
        }
    }

    public void ClipPlay_Immediate(int clipIndex)
    {
        GameManager.Instance.AudioPlay(audioCaption[clipIndex].clip);
        captionPanel.SetActive(true);
        //captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = audioCaption[clipIndex].caption;
        float closeTime = audioCaption[clipIndex].clip.length;

        StopAllCoroutines();
        StartCoroutine(CloseCaption(closeTime));
        StartCoroutine(CharacterDialogue(audioCaption[clipIndex].caption));
    }

    public void ClipPlay_Immediate(AudioCaptionMix audioCaption)
    {
        GameManager.Instance.AudioPlay(audioCaption.clip);
        captionPanel.SetActive(true);
        //captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = audioCaption[clipIndex].caption;
        float closeTime = audioCaption.clip.length;

        StopAllCoroutines();
        StartCoroutine(CloseCaption(closeTime));
        StartCoroutine(CharacterDialogue(audioCaption.caption));
    }

    public void ClipPlay_Delay(int clipIndex, float delay)
    {
        StartCoroutine(DelayAudio(clipIndex, delay));
    }

    IEnumerator DelayAudio(int clipIndex, float delay)
    {
        yield return new WaitForSeconds(delay);

        if (audioCaption[clipIndex].clip != null)
        { 
            GameManager.Instance.AudioPlay(audioCaption[clipIndex].clip);
        }
        else
        {
            GameManager.Instance.AudioPlay(audioClips_Private[clipIndex]);
        }
        captionPanel.SetActive(true);
        //captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = audioCaption[clipIndex].caption;

        float closeTime = 0f;
        if (audioCaption[clipIndex].clip != null)
        { 
            closeTime = audioCaption[clipIndex].clip.length;
        }
        else { 
               closeTime = closeTime_Private[clipIndex];
        }

        StopAllCoroutines();
        StartCoroutine(CloseCaption(closeTime));
        StartCoroutine(CharacterDialogue(audioCaption[clipIndex].caption));
    }

    IEnumerator CloseCaption(float delay)
    {
        yield return new WaitForSeconds(delay);
        captionPanel.SetActive(false);
    }

    IEnumerator CharacterDialogue(string dialogue)
    {
        captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text = "";
        foreach (char letter in dialogue.ToCharArray())
        {
            captionPanel.transform.GetChild(0).GetComponent<TMP_Text>().text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
