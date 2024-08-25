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
    [SerializeField] private Sprite[] bw_Area;
    [SerializeField] private Material[] bw_material;
    [SerializeField] private SpriteRenderer[] colored_Area;
    [SerializeField] private GameObject park_BW;

    [Space(10)]
    [Header("Trees")]
    [SerializeField] private GameObject trees;

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
        
    }

    void TransitionSprites_OnStart()
    { 

    }

    void TransitionSprites_OnEnd(PlayableDirector obj)
    {
        for(int i = 0; i < colored_Area.Length; i++)
        {
            colored_Area[i].sprite = bw_Area[i];
            colored_Area[i].material = bw_material[i];
        }
        trees.SetActive(false);
        park_BW.SetActive(false);
    }
}
