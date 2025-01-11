using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ConfirmationPopUp : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    private int count = 0; 

    public void PanelActivate() 
    {
        panel.SetActive(true);
        if (count == 0)
        { 
            panel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Do I have everything for coffee?";
            GameManager.Instance.gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(4);
        }
        else if(count == 1)
        {
            panel.transform.GetChild(1).gameObject.GetComponent<TMP_Text>().text = "Would it be safe inside?";
            GameManager.Instance.gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(5);
        }
    }

    public void No()
    {
        panel.SetActive(false);
    }

    public void Yes()
    {
        if (count == 0)
        {
            count++;
            PanelActivate();
        }
        else if (count == 1)
        {
            GameManager.Instance.movementSystem.ConfirmationPopUp_Yes();
            GameManager.Instance.audioSrc.Stop();
            panel.SetActive(false);
        }
    }
}
