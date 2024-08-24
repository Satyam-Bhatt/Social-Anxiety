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
    [SerializeField] private GameObject[] areas;
    [SerializeField] private Sprite[] areaSprites;

    private void OnEnable()
    {
        timeline.stopped += TransitionSprites;
    }

    private void OnDisable()
    {
        timeline.stopped -= TransitionSprites;
    }

    void TransitionSprites(PlayableDirector timeline)
    { 
        for(int i = 0; i < areas.Length; i++)
        {
            areas[i].GetComponent<SpriteRenderer>().sprite = areaSprites[i];
        }
    }
}
