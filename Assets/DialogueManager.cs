using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox_Sentence;
    [SerializeField] private TMP_Text textBox_Name;

    //[TextArea(3, 10)]
    //[SerializeField] private string[] dialogues;

    int dialogueIndex = 0;
    int dialogueLength = 0;

    public bool bro = false;

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
    private void Start()
    {
        //charDialogue = CharacterDialogue(dialogues[dialogueIndex]);
    }

    public void DialogueCharacter(string[] dialogues)
    {
        dialogueLength = dialogues.Length;
        StopAllCoroutines();
        StartCoroutine(CharacterDialogue(dialogues[dialogueIndex], dialogues));
    }

    private void Update()
    { 
    
    }

    public void ConversationStar()
    {
        Debug.Log("Conversation Start");
    }

    IEnumerator CharacterDialogue(string dialogue, string[] dialogues)
    {
        textBox_Sentence.text = "";
        foreach (char letter in dialogue.ToCharArray())
        { 
            textBox_Sentence.text += letter;
            yield return new WaitForSeconds(0.1f);
        }

        if (dialogueIndex < dialogueLength)
        { 
            yield return new WaitForSeconds(1f);
            DialogueCharacter(dialogues);
            dialogueIndex++;        
        }
    }
}
