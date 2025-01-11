using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CheckScript : MonoBehaviour
{
    float value = 0;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Image>().fillAmount = value;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            if (value <= 0)
            { 
                value += 0.15f;
            }
            value += 6f * Time.deltaTime;
            GetComponent<Image>().fillAmount = value;
        }
        else if(value > 0)
        { 
            value -= 0.05f * Time.deltaTime;
            GetComponent<Image>().fillAmount = value;
        }
    }
}
