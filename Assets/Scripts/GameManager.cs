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

    [HideInInspector]
    public bool coffeeGameDone = false;

    public TMP_Text tasks;

    private int counter = 0;

    public AudioSource audioSrc;
    public static GameManager Instance
    { get
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
    }

    private void OnDisable()
    {
        movementSystem.onTimelineStart -= BW_Transition;

        playerControls.CoffeGame.EyesClose.Disable();
        playerControls.CoffeGame.EyesClose.started -= EyesFollow;
        playerControls.CoffeGame.EyesClose.canceled -= EyesFollow;
    }

    private void Awake()
    {
        playerControls = new PlayerControls();
        audioSrc = GetComponent<AudioSource>();
    }

    private void Start()
    {
        tasks.text = "- Go to the park";
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
        Application.Quit();
    }

    public void No(GameObject open)
    {
        counter++;
        if (counter == 3)
        {
            open.SetActive(true);
        }
        else {
            InventoryManager.Instance.AfterSleep();
        }
    }
    public void Replay()
    {
        string s = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(s);
    }
       
}
