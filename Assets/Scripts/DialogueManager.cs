using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Playables;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox_Sentence;
    [SerializeField] private TMP_Text textBox_Name;
    [SerializeField] private PlayableDirector timeline;

    //[TextArea(3, 10)]
    //[SerializeField] private string[] dialogues;

    int dialogueIndex = 0;
    int dialogueLength = 0;
    string previousName = "";
    Vector2[] dialoguePosition = new Vector2[2];

    private static DialogueManager _instance;

    public static DialogueManager Instance 
    {
        get
        {
            _instance = FindObjectOfType<DialogueManager>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<DialogueManager>();
            }

            return _instance;
        }
    }

    private AudioClip[] clips_This;

    private void Start()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }

    public void DialogueCharacter(string[] dialogues, string[] name, Vector2[] dialoguePosition, AudioClip[] clips)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        textBox_Name.text = name[dialogueIndex];
        this.dialoguePosition = dialoguePosition;
        clips_This = clips;

        GameManager.Instance.AudioPlay(clips[dialogueIndex]);

        if (previousName == null)
        {
            previousName = name[dialogueIndex];
            GetComponent<RectTransform>().anchoredPosition = dialoguePosition[0];
        }
        else if (previousName != name[dialogueIndex])
        {
            previousName = name[dialogueIndex];
            if (textBox_Name.alignment == TextAlignmentOptions.Left)
            {
                textBox_Name.alignment = TextAlignmentOptions.Right;
                textBox_Sentence.alignment = TextAlignmentOptions.TopRight;
            }
            else
            {
                textBox_Name.alignment = TextAlignmentOptions.Left;
                textBox_Sentence.alignment = TextAlignmentOptions.TopLeft;
            }

            if (GetComponent<RectTransform>().anchoredPosition == dialoguePosition[0]) GetComponent<RectTransform>().anchoredPosition = dialoguePosition[1];
            else GetComponent<RectTransform>().anchoredPosition = dialoguePosition[0];
        }    

        dialogueLength = dialogues.Length;
        StopAllCoroutines();
        StartCoroutine(CharacterDialogue(dialogues[dialogueIndex], dialogues, name));
    }

    IEnumerator CharacterDialogue(string dialogue, string[] dialogues, string[] name)
    {
        textBox_Sentence.text = "";
        foreach (char letter in dialogue.ToCharArray())
        { 
            textBox_Sentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        dialogueIndex++;
        if (dialogueIndex < dialogueLength)
        {
            while (GameManager.Instance.audioSrc.isPlaying)
            { 
                yield return null;
            }

            yield return new WaitForSeconds(1f);
            DialogueCharacter(dialogues, name, dialoguePosition, clips_This);
        }
        else 
        {
            yield return new WaitForSeconds(1f);
            dialogueIndex = 0;
            timeline.Play();
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
        }
    }
}
