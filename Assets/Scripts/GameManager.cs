using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public MovementSystem movementSystem;

    private PlayerControls playerControls;
    public bool eyesShut { get; private set; } = false;

    public bool isBW;// { get; private set; } = false;
    public bool endGame = false;

    //[HideInInspector]
    public bool coffeeGameDone = false;

    public TMP_Text tasks;

    private int counter = 0;

    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject winPanel;

    public AudioSource audioSrc;
    public static GameManager Instance
    {
        get
        {
            _instance = FindObjectOfType<GameManager>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<GameManager>();
            }

            return _instance;
        }
    }

    private void OnEnable()
    {
        movementSystem = FindObjectOfType<MovementSystem>();
        movementSystem.onTimelineStart += BW_Transition;

        playerControls.CoffeGame.EyesClose.Enable();
        playerControls.CoffeGame.EyesClose.started += EyesFollow;
        playerControls.CoffeGame.EyesClose.canceled += EyesFollow;

        playerControls.GeneralNavigation.PauseGame.Enable();
        playerControls.GeneralNavigation.PauseGame.started += Pause;
    }

    private void OnDisable()
    {
        movementSystem.onTimelineStart -= BW_Transition;

        playerControls.CoffeGame.EyesClose.Disable();
        playerControls.CoffeGame.EyesClose.started -= EyesFollow;
        playerControls.CoffeGame.EyesClose.canceled -= EyesFollow;

        playerControls.GeneralNavigation.PauseGame.Disable();
        playerControls.GeneralNavigation.PauseGame.started -= Pause;

    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        tasks.text = "- Go to the park";

        deathPanel.SetActive(false);
        pausePanel.SetActive(false);
        winPanel.SetActive(false);
    }

    public void ActivateWinPane()
    {
        winPanel.SetActive(true);
    }

    public void BW_Transition()
    {
        //Code related to all the changes after world turn BW
        //Food doesn't make happy
        Debug.Log("BW Transition");
        isBW = true;
    }

    public void EyesFollow(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            eyesShut = true;
        }
        else if (context.canceled)
        {
            eyesShut = false;
        }
    }

    public void AudioPlay(AudioClip clip)
    {
        //audioSrc.clip = clip;
        audioSrc.Stop();
        audioSrc.PlayOneShot(clip);
    }

    public void Yes()
    {
        gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(12);
        //Application.Quit();
        deathPanel.SetActive(true);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void No(GameObject open)
    {
        counter++;

        if (counter == 1)
        {
            gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(9);
        }
        else if (counter == 2)
        {
            gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(10);
        }
        else if (counter == 3)
        {
            //open.SetActive(true);
            //gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(11);
            //AudioManager.Instance.AudioPlay(AudioManager.Instance.beforeBW_Clip);
        }
        //InventoryManager.Instance.AfterSleep();

    }
    public void Replay()
    {
        string s = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(s);
        Time.timeScale = 1f;
    }

    [Space(10)]
    [Header("Pause Game")]
    [SerializeField] private GameObject pausePanel;
    private bool pause = false;
    public void Pause(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (!pause)
            {
                pause = true;
                pausePanel.SetActive(true);
                Time.timeScale = 0f;
            }
            else
            {
                pause = false;
                pausePanel.SetActive(false);
                Time.timeScale = 1f;
            }
        }
    }

    public void Pause()
    {
        if (!pause)
        {
            pause = true;
            pausePanel.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            pause = false;
            pausePanel.SetActive(false);
            Time.timeScale = 1f;
        }

    }



}
