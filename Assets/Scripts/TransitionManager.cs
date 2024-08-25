 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class TransitionManager : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private PlayableDirector timeline;

    [Space(10)]
    [Header("Areas")]
    [SerializeField] private GameObject bw_Area;
    [SerializeField] private SpriteRenderer[] colored_Area; 

    [SerializeField] private MovementSystem movementSystem;

    private void OnEnable()
    {
        movementSystem.onTimelineStart += TransitionSprites_OnStart;
        timeline.stopped += TransitionSprites_OnEnd;
    }

    private void OnDisable()
    {
        movementSystem.onTimelineStart -= TransitionSprites_OnStart;
        timeline.stopped -= TransitionSprites_OnEnd;
    }

    private void Start()
    {
        bw_Area.SetActive(false);
    }

    void TransitionSprites_OnStart()
    { 
        bw_Area.SetActive(true);
    }

    void TransitionSprites_OnEnd(PlayableDirector obj)
    {
        foreach (SpriteRenderer area in colored_Area)
        {
            area.sprite = null;
            area.material = null;
        }
    }
}
