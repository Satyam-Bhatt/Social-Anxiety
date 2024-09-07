using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    private MovementSystem movementSystem;

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
    }

    private void OnDisable()
    {
        movementSystem.onTimelineStart -= BW_Transition;
    }


    public void BW_Transition()
    {
        //Code related to all the changes after world turn BW
        //Food doesn't make happy
        Debug.Log("BW Transition");
    }

}