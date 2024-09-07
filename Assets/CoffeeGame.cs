using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CoffeeGame : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Image image;

    private TMP_Text letterToPress;

    private float value = 0;
    private int keyIndex = 1;

    private void Awake()
    {
        playerControls = new PlayerControls();
        letterToPress = image.transform.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        image.fillAmount = 0;
        letterToPress.text = "K";
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.FirstPress.Enable();
        playerControls.CoffeGame.FirstPress.started += FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled += FirstPressed;

        playerControls.CoffeGame.SecondPress.started += FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled += FirstPressed;

        playerControls.CoffeGame.ThirdPress.started += FirstPressed;
        playerControls.CoffeGame.ThirdPress.canceled += FirstPressed;

        playerControls.CoffeGame.FourthPress.started += FirstPressed;
        playerControls.CoffeGame.FourthPress.canceled += FirstPressed;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.FirstPress.Disable();
        playerControls.CoffeGame.FirstPress.started -= FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled -= FirstPressed;

        playerControls.CoffeGame.SecondPress.Disable();
        playerControls.CoffeGame.SecondPress.started -= FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled -= FirstPressed;

        playerControls.CoffeGame.ThirdPress.Disable();
        playerControls.CoffeGame.ThirdPress.started -= FirstPressed;
        playerControls.CoffeGame.ThirdPress.canceled -= FirstPressed;

        playerControls.CoffeGame.FourthPress.Disable();
        playerControls.CoffeGame.FourthPress.started -= FirstPressed;
        playerControls.CoffeGame.FourthPress.canceled -= FirstPressed;
    }



    public void FirstPressed(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            StopAllCoroutines();
            StartCoroutine(ValueChange(0.1f));
        }
        else 
        {
            StopAllCoroutines();
            StartCoroutine(ValueChange(-0.5f));
        }
    }

    IEnumerator ValueChange(float incrementDecrement)
    {
        while (value >= 0 && value <= 1)
        {
            value += incrementDecrement * Time.deltaTime;
            image.fillAmount = value;

            if (value >= 1f)
            {
                if (keyIndex == 1)
                {
                    playerControls.CoffeGame.FirstPress.Disable();
                    playerControls.CoffeGame.SecondPress.Enable();
                    keyIndex++;
                    letterToPress.text = "L";
                }
                else if (keyIndex == 2)
                {
                    playerControls.CoffeGame.SecondPress.Disable();
                    playerControls.CoffeGame.ThirdPress.Enable();
                    keyIndex++;
                    letterToPress.text = "Y";
                }
                else if (keyIndex == 3)
                { 
                    playerControls.CoffeGame.ThirdPress.Disable();
                    playerControls.CoffeGame.FourthPress.Enable();
                    keyIndex++;
                    letterToPress.text = "V";
                }
                else if(keyIndex == 4)
                {
                    playerControls.CoffeGame.FourthPress.Disable();
                    keyIndex++;
                }
                break;
            }

            yield return new WaitForEndOfFrame();
        }
        Debug.Log("Coroutine Stop");
        value = 0;
        image.fillAmount = value;
        StopAllCoroutines();
    }
}