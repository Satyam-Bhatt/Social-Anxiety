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
    public int keyIndex = 1;
    public bool playOnce = true;

    public Action onCoffeeGameCompleted;

    [SerializeField] private GameObject eyeGame;

    public float incrementValue = 0.007f;
    public bool firstAudioPlay = false;

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
/*        if (canPlay)
        {
            if (context.started)
            {
                StopAllCoroutines();

                if (value <= 0)
                {
                    value += 0.06f;
                }
                value += incrementValue * Time.fixedDeltaTime;
                //StartCoroutine(IncreaseValue());
                image.fillAmount = value;


                //image.transform.position = imagePosition;
                //StartCoroutine(ValueChange(0.1f));
                if (firstAudioPlay == false)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(15);
                    firstAudioPlay = true;
                }

                if (keyIndex == 2 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(16);
                    playOnce = true;
                }
                else if (keyIndex == 3 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(17);
                    playOnce = true;
                }
                else if (keyIndex == 4 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(18);
                    playOnce = true;
                }

                KeyEnabler();

            }
            else
            {
                StopAllCoroutines();
                StartCoroutine(DecreaseValue());

                //image.transform.position = imagePosition;
                //StartCoroutine(ValueChange(-0.5f));
            }
        }*/
    }

    IEnumerator DecreaseValue()
    {
        yield return new WaitForSeconds(0.5f);
        while (value > 0 && value <= 1 && canPlay)
        {
            value -= 0.05f * Time.fixedDeltaTime;
            image.fillAmount = value;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator IncreaseValue()
    {
        while (value <= 1 && canPlay)
        {
            value += incrementValue * Time.fixedDeltaTime;
            image.fillAmount = value;

            if (value >= 1)
            {
                KeyEnabler();
            }
            yield return new WaitForEndOfFrame();
        }
    }

    public void EnableSprites()
    {
        if (keyIndex == 1)
        {
            keyIndex++;

            coffeeActivator[0].SetActive(false);
            coffeeActivator[1].SetActive(true);

        }
        else if (keyIndex == 2)
        {
            keyIndex++;

            coffeeActivator[1].SetActive(false);
            coffeeActivator[2].SetActive(true);
        }
        else if (keyIndex == 3)
        {
            keyIndex++;

            coffeeActivator[2].SetActive(false);
            coffeeActivator[3].SetActive(true);
        }
        else if (keyIndex == 4)
        {
            keyIndex++;

            coffeeActivator[3].SetActive(false);
            eyeGame.SetActive(false);
            GameManager.Instance.coffeeGameDone = true;
            GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);
            GameManager.Instance.tasks.text = "- Get rid of thoughts";

            onCoffeeGameCompleted?.Invoke();
        }

        GetComponent<MovementSystem>().DisableEnableMovementInput(true);

        StopAllCoroutines();

    }

    void KeyEnabler()
    {
        if (value >= 1f)
        {
            if (keyIndex == 1)
            {
                incrementValue = 0.2f;
                playerControls.CoffeGame.FirstPress.Disable();
                playerControls.CoffeGame.SecondPress.Enable();
                keyIndex++;
                letterToPress.text = "L";

                coffeeActivator[0].SetActive(false);
                coffeeActivator[1].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[1].transform.position.x - offset, coffeeActivator[1].transform.position.y, coffeeActivator[1].transform.position.z);
                playOnce = false;
            }
            else if (keyIndex == 2)
            {
                incrementValue = 0.2f;
                playerControls.CoffeGame.SecondPress.Disable();
                playerControls.CoffeGame.ThirdPress.Enable();
                keyIndex++;
                letterToPress.text = "Y";

                coffeeActivator[1].SetActive(false);
                coffeeActivator[2].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[2].transform.position.x - offset, coffeeActivator[2].transform.position.y, coffeeActivator[2].transform.position.z);
                playOnce = false;
            }
            else if (keyIndex == 3)
            {
                incrementValue = 0.2f;
                playerControls.CoffeGame.ThirdPress.Disable();
                playerControls.CoffeGame.FourthPress.Enable();
                keyIndex++;
                letterToPress.text = "V";

                coffeeActivator[2].SetActive(false);
                coffeeActivator[3].SetActive(true);
                image.transform.position = new Vector3(coffeeActivator[3].transform.position.x - offset, coffeeActivator[3].transform.position.y, coffeeActivator[3].transform.position.z);
                playOnce = false;
            }
            else if (keyIndex == 4)
            {
                incrementValue = 0.2f;
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
}
