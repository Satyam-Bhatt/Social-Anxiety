using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimelineController : MonoBehaviour
{
    private PlayableDirector timeline;
    [SerializeField] private MovementSystem player;

    private void Awake()
    {
        timeline = GetComponent<PlayableDirector>();
    }

    private void Start()
    {
        player.cutscenePlaying = true;
    }

    private void OnEnable()
    {
        timeline.stopped += TimelineEnd;
    }

    private void OnDisable()
    { 
        timeline.stopped -= TimelineEnd;
    }

    public void TimelineEnd(PlayableDirector obj)
    {
        player.cutscenePlaying = false;
        GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Delay(3, 1f);
    }
}
