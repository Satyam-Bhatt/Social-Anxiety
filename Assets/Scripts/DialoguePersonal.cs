using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class DialoguePersonal : MonoBehaviour
{
    [SerializeField] private string[] names;

    [TextArea(3, 10)]
    [SerializeField] private string[] sentences;

    [SerializeField] private Vector2[] dialoguePosition = new Vector2[2];

    [SerializeField] private PlayableDirector timeline;


    // Update is called once per frame
    public void DialogueStart()
    {
        Debug.Log("dialogue start");
        timeline.Pause();

        DialogueManager.Instance.DialogueCharacter(sentences, names, dialoguePosition);
    }
}
