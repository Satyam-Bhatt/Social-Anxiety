using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;

public class CoffeeGame : MonoBehaviour
{
    private PlayerControls playerControls;

    [SerializeField] private Image image;

    private float value = 0;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void Start()
    {
        image.fillAmount = 0;
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.FirstPress.Enable();
        playerControls.CoffeGame.FirstPress.started += FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled += FirstPressed;

        playerControls.CoffeGame.SecondPress.Enable();
        playerControls.CoffeGame.SecondPress.started += FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled += FirstPressed;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.FirstPress.Disable();
        playerControls.CoffeGame.FirstPress.started -= FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled -= FirstPressed;

        playerControls.CoffeGame.SecondPress.Disable();
        playerControls.CoffeGame.SecondPress.started -= FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled -= FirstPressed;
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
            Debug.Log(value);
            image.fillAmount = value;

            if (value >= 1f)
            {
                playerControls.CoffeGame.FirstPress.Disable();
                playerControls.CoffeGame.FirstPress.started -= FirstPressed;
                playerControls.CoffeGame.FirstPress.canceled -= FirstPressed;
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
