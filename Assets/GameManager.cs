using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private MovementSystem movementSystem;

    private PlayerControls playerControls;
    public bool eyesShut { get; private set; } = false;

    public bool isBW { get; private set; } = false;

    [HideInInspector]
    public bool coffeeGameDone = false;

    public TMP_Text tasks;

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
        movementSystem  = FindObjectOfType<MovementSystem>();
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

}
