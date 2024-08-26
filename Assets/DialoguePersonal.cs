using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePersonal : MonoBehaviour
{
    [SerializeField] private string[] names;

    [TextArea(3, 10)]
    [SerializeField] private string[] sentences;


    // Update is called once per frame
    public void DialogueStart()
    {
        Debug.Log("dialogue start");
        DialogueManager.Instance.DialogueCharacter(sentences);
        //DialogueManager.Instance.bro = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            DialogueStart();
        }
    }
}
