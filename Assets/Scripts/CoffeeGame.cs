using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;

public class CoffeeGame : MonoBehaviour
{
    private PlayerControls playerControls;

    public Image image;

    public bool canPlay = true;

    public float offset = 0f;

    public GameObject[] coffeeActivator = new GameObject[4];

    private TMP_Text letterToPress;

    private float value = 0;
    private int keyIndex = 1;

    public Action onCoffeeGameCompleted;

    [SerializeField] private GameObject eyeGame;

    private void Awake()
    {
        playerControls = new PlayerControls();
        letterToPress = image.transform.GetComponentInChildren<TMP_Text>();
    }

    private void Start()
    {
        image.gameObject.SetActive(true);

        image.transform.position = new Vector3(coffeeActivator[0].transform.position.x - offset, coffeeActivator[0].transform.position.y, coffeeActivator[0].transform.position.z);

        image.fillAmount = 0;
        letterToPress.text = "K";

        coffeeActivator[0].SetActive(true);
        coffeeActivator[1].SetActive(false);
        coffeeActivator[2].SetActive(false);
        coffeeActivator[3].SetActive(false);
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.FirstPress.Enable();
        playerControls.CoffeGame.FirstPress.started += FirstPressed;
        playerControls.CoffeGame.FirstPress.performed += FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled += FirstPressed;

        playerControls.CoffeGame.SecondPress.started += FirstPressed;
        playerControls.CoffeGame.SecondPress.performed += FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled += FirstPressed;

        playerControls.CoffeGame.ThirdPress.started += FirstPressed;
        playerControls.CoffeGame.ThirdPress.performed += FirstPressed;
        playerControls.CoffeGame.ThirdPress.canceled += FirstPressed;

        playerControls.CoffeGame.FourthPress.started += FirstPressed;
        playerControls.CoffeGame.FourthPress.performed += FirstPressed;
        playerControls.CoffeGame.FourthPress.canceled += FirstPressed;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.FirstPress.Disable();
        playerControls.CoffeGame.FirstPress.started -= FirstPressed;
        playerControls.CoffeGame.FirstPress.performed -= FirstPressed;
        playerControls.CoffeGame.FirstPress.canceled -= FirstPressed;

        playerControls.CoffeGame.SecondPress.Disable();
        playerControls.CoffeGame.SecondPress.started -= FirstPressed;
        playerControls.CoffeGame.SecondPress.performed -= FirstPressed;
        playerControls.CoffeGame.SecondPress.canceled -= FirstPressed;

        playerControls.CoffeGame.ThirdPress.Disable();
        playerControls.CoffeGame.ThirdPress.started -= FirstPressed;
        playerControls.CoffeGame.ThirdPress.performed -= FirstPressed;
        playerControls.CoffeGame.ThirdPress.canceled -= FirstPressed;

        playerControls.CoffeGame.FourthPress.Disable();
        playerControls.CoffeGame.FourthPress.started -= FirstPressed;
        playerControls.CoffeGame.FourthPress.performed -= FirstPressed;
        playerControls.CoffeGame.FourthPress.canceled -= FirstPressed;
    }

    public void FirstPressed(InputAction.CallbackContext context)
    {
        if (canPlay)
        {
            if (context.started)
            {
                StopAllCoroutines();

                if (value <= 0)
                {
                    value += 0.1f;
                }
                value += 6f * Time.deltaTime;
                image.fillAmount = value;
                KeyEnabler();

                //image.transform.position = imagePosition;
                //StartCoroutine(ValueChange(0.1f));
            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(DecreaseValue());

                //image.transform.position = imagePosition;
                //StartCoroutine(ValueChange(-0.5f));
            }
        }
    }

    IEnumerator DecreaseValue()
    {
        while (value > 0 && value <= 1 && canPlay)
        {
            value -= 0.1f * Time.deltaTime;
            image.fillAmount = value;
            yield return new WaitForEndOfFrame();
        }
    }

    void KeyEnabler()
    {
        if (value >= 1f)
        {
            if (keyIndex == 1)
            {
                playerControls.CoffeGame.FirstPress.Disable();
                playerControls.CoffeGame.SecondPress.Enable();
                keyIndex++;
                letterToPress.text = "L";

                coffeeActivator[0].SetActive(false);
                coffeeActivator[1].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[1].transform.position.x - offset, coffeeActivator[1].transform.position.y, coffeeActivator[1].transform.position.z);

                //Audio Two
            }
            else if (keyIndex == 2)
            {
                playerControls.CoffeGame.SecondPress.Disable();
                playerControls.CoffeGame.ThirdPress.Enable();
                keyIndex++;
                letterToPress.text = "Y";

                coffeeActivator[1].SetActive(false);
                coffeeActivator[2].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[2].transform.position.x - offset, coffeeActivator[2].transform.position.y, coffeeActivator[2].transform.position.z);

                //Audio Three
            }
            else if (keyIndex == 3)
            {
                playerControls.CoffeGame.ThirdPress.Disable();
                playerControls.CoffeGame.FourthPress.Enable();
                keyIndex++;
                letterToPress.text = "V";

                coffeeActivator[2].SetActive(false);
                coffeeActivator[3].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[3].transform.position.x - offset, coffeeActivator[3].transform.position.y, coffeeActivator[3].transform.position.z);

                //Audio Four
            }
            else if (keyIndex == 4)
            {
                playerControls.CoffeGame.FourthPress.Disable();
                keyIndex++;

                coffeeActivator[3].SetActive(false);
                eyeGame.SetActive(false);
                GameManager.Instance.coffeeGameDone = true;
                GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);
                GameManager.Instance.tasks.text = "- Get rid of thoughts";

                onCoffeeGameCompleted?.Invoke();
            }

            value = 0;
            image.fillAmount = value;
            StopAllCoroutines();

        }
    }

    IEnumerator ValueChange(float incrementDecrement)
    {
        while (value >= 0 && value <= 1 && canPlay)
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

                    coffeeActivator[0].SetActive(false);
                    coffeeActivator[1].SetActive(true);
                    image.transform.position = new Vector3(coffeeActivator[1].transform.position.x - offset, coffeeActivator[1].transform.position.y, coffeeActivator[1].transform.position.z);
                }
                else if (keyIndex == 2)
                {
                    playerControls.CoffeGame.SecondPress.Disable();
                    playerControls.CoffeGame.ThirdPress.Enable();
                    keyIndex++;
                    letterToPress.text = "Y";

                    coffeeActivator[1].SetActive(false);
                    coffeeActivator[2].SetActive(true);
                    image.transform.position = new Vector3(coffeeActivator[2].transform.position.x - offset, coffeeActivator[2].transform.position.y, coffeeActivator[2].transform.position.z);
                }
                else if (keyIndex == 3)
                {
                    playerControls.CoffeGame.ThirdPress.Disable();
                    playerControls.CoffeGame.FourthPress.Enable();
                    keyIndex++;
                    letterToPress.text = "V";

                    coffeeActivator[2].SetActive(false);
                    coffeeActivator[3].SetActive(true);
                    image.transform.position = new Vector3(coffeeActivator[3].transform.position.x - offset, coffeeActivator[3].transform.position.y, coffeeActivator[3].transform.position.z);
                }
                else if (keyIndex == 4)
                {
                    playerControls.CoffeGame.FourthPress.Disable();
                    keyIndex++;

                    coffeeActivator[3].SetActive(false);
                    eyeGame.SetActive(false);
                    GameManager.Instance.coffeeGameDone = true;
                    GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);
                    GameManager.Instance.tasks.text = "- Get rid of thoughts";

                    onCoffeeGameCompleted?.Invoke();
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
