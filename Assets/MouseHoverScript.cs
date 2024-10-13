using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseHoverScript : MonoBehaviour, IPointerEnterHandler
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void OnEnable()
    {
        //button.OnPointerEnter += PointerEnter;
       
    }

    private void Update()
    {
        //if (button.OnPointerEnter())
        //{ 
            
        //}

      //  var enter = button.OnPointerEnter();
    }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log(data);
    }
}
