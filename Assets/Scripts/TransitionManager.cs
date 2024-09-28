 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

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

    [Space(10)]
    [Header("Characters")]
    [SerializeField] private GameObject characters;
    [SerializeField] private GameObject characters_BW;

    [Space(10)]
    [SerializeField] private MovementSystem movementSystem;

    [Space(10)]
    [Header("Coffee Game")]
    public GameObject coffeeGame;

    [Space(10)]
    [Header("Paper")]
    public Sprite crumbledPaper;
    public GameObject notesPanel;

    [Space(10)]
    [Header("Global Volume")]
    [SerializeField] private GameObject globalVolume;

    private static TransitionManager _instance;

    public static TransitionManager Instance
    {
        get
        {
            _instance = FindObjectOfType<TransitionManager>();
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<TransitionManager>();
            }

            return _instance;
        }
    }

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
        characters_BW.SetActive(false);
        coffeeGame.SetActive(false);
    }

    void TransitionSprites_OnStart()
    { 
        StartCoroutine(CharacterSwitch());
    }

    void TransitionSprites_OnEnd(PlayableDirector obj)
    {
        for(int i = 0; i < colored_Area.Length; i++)
        {
            colored_Area[i].sprite = bw_Area[i];
            colored_Area[i].material = bw_material[0];
        }       
        park_BW.SetActive(false);
        coffeeGame.SetActive(true);

        notesPanel.GetComponent<Image>().sprite = crumbledPaper;

        globalVolume.SetActive(true);
    }

    IEnumerator CharacterSwitch()
    {
        yield return new WaitForSeconds(3f);
        trees.SetActive(false);
        characters_BW.SetActive(true);
        characters.SetActive(false);
    }
}
