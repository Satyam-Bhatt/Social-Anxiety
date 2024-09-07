using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePersonal : MonoBehaviour
{
    [SerializeField] private string[] names;

    [TextArea(3, 10)]
    [SerializeField] private string[] sentences;

    [SerializeField] private Vector2[] dialoguePosition = new Vector2[2];


    // Update is called once per frame
    public void DialogueStart()
    {
        Debug.Log("dialogue start");
        DialogueManager.Instance.DialogueCharacter(sentences, names, dialoguePosition);
    }
}
