using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private TMP_Text textBox;

    [TextArea(3, 10)]
    [SerializeField] private string[] dialogues;

    private IEnumerator charDialogue;

    int dialogueIndex = 0;
    private void Start()
    {
        //charDialogue = CharacterDialogue(dialogues[dialogueIndex]);
    }

    public void DialogueCharacter(string[] dialogues, TMP_Text textBox)
    {
        StartCoroutine(CharacterDialogue(dialogues[dialogueIndex]));
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            if(dialogueIndex < dialogues.Length)
            {
                DialogueCharacter(dialogues, textBox);
                dialogueIndex++;
            }
            else
            {
                StopAllCoroutines();
            }

        }
    }

    IEnumerator CharacterDialogue(string dialogue)
    {
        textBox.text = "";
        foreach (char letter in dialogue.ToCharArray())
        { 
            textBox.text += letter;
            yield return new WaitForSeconds(0.1f);
        }
    }
}
