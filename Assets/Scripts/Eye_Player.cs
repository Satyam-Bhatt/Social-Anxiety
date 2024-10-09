using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Playables;

public class Eye_Player : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private Sprite[] EyesClose_Open = new Sprite[2];
    private SpriteRenderer s;

    public bool position = false;

    private float value = 0f;
    [SerializeField] private Material mat;

    [SerializeField] private MovementSystem movementSystem;
    private PlayableDirector timeline;

    [SerializeField] private GameObject[] active_deactive;

    private Vector3 posiStore = Vector3.zero;

    [SerializeField] private Transform rect;

    [SerializeField] private GameObject rmb_ToStart;
    [SerializeField] private GameObject spawner;

    private CoffeeGame coffeeGame;

    private bool once = false;
    private AudioSource audioSrc;
    [SerializeField] private AudioClip audioClip;
    private bool invinsible = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        s = gameObject.transform.GetComponent<SpriteRenderer>();
        timeline = GetComponent<PlayableDirector>();
        coffeeGame = movementSystem.GetComponent<CoffeeGame>();
        audioSrc = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {

        if (posiStore == Vector3.zero)
        {
            posiStore = transform.localPosition;
        }
        else
        {
            transform.localPosition = posiStore;
        }

        mat.SetFloat("_Anxiety", 0f);
        rmb_ToStart.SetActive(true);
        spawner.SetActive(false);

        playerControls.CoffeGame.EyesClose.Enable();
        playerControls.CoffeGame.EyesClose.started += SpriteChange;
        playerControls.CoffeGame.EyesClose.canceled += SpriteChange;

        timeline.stopped += TimelineStopped;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.EyesClose.Disable();
        playerControls.CoffeGame.EyesClose.started -= SpriteChange;
        playerControls.CoffeGame.EyesClose.canceled -= SpriteChange;
    }

    private void Start()
    {
        s.sprite = EyesClose_Open[0];
        mat.SetFloat("_Anxiety", 0f);
    }

    private void Update()
    {
        if (position)
        {
            float rightX = rect.transform.position.x + rect.transform.lossyScale.x / 2;
            float leftX = rect.transform.position.x - rect.transform.lossyScale.x / 2;
            float topY = rect.transform.position.y + rect.transform.lossyScale.y / 2;
            float bottomY = rect.transform.position.y - rect.transform.lossyScale.y / 2;
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (mousePosition.x < rightX && mousePosition.x > leftX && mousePosition.y < topY && mousePosition.y > bottomY)
            {
                transform.position = Vector3.Lerp(transform.position, mousePosition, 30 * Time.deltaTime);
            }
        }
    }

    public void SpriteChange(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            s.sprite = EyesClose_Open[1];
            position = true;

            rmb_ToStart.SetActive(false);
            spawner.SetActive(true);

            if (once == false)
            {
                movementSystem.GetComponent<CoffeeGame>().incrementValue = 0.2f;
                once = true;
            }
        }
        else
        {
            s.sprite = EyesClose_Open[0];
            position = false;

            rmb_ToStart.SetActive(true);
            spawner.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Eyes") && !invinsible)
        {
            mat.SetFloat("_Anxiety", value += 0.1f);
            audioSrc.Stop();
            audioSrc.PlayOneShot(audioClip);
            StartCoroutine(InvinsibilityFrames());
        }

        if (value >= 1f)
        {
            timeline.Play();
            GameManager.Instance.GetComponent<AudioSource>().Stop();
            GameManager.Instance.GetComponent<RandomThoughts>().AudioCloseReset();

            if (coffeeGame.keyIndex > 1)
            {
                coffeeGame.playOnce = false;
            }
            else if (coffeeGame.keyIndex == 1)
            { 
                coffeeGame.firstAudioPlay = false;
            }

            movementSystem.cutscenePlaying = true;

            foreach (GameObject g in active_deactive)
            {
                g.SetActive(false);
            }
            movementSystem.gameObject.GetComponent<CoffeeGame>().coffeeActivator[0].transform.parent.gameObject.SetActive(false);
            movementSystem.gameObject.GetComponent<CoffeeGame>().canPlay = false;
            movementSystem.gameObject.GetComponent<CoffeeGame>().image.gameObject.SetActive(false);
            GameManager.Instance.tasks.transform.parent.gameObject.SetActive(false);
        }
    }

    public void TimelineStopped(PlayableDirector timeline)
    {
        movementSystem.cutscenePlaying = false;
        transform.parent.transform.parent.transform.parent.Find("Trigger").gameObject.SetActive(true);

        foreach (GameObject g in active_deactive)
        {
            g.SetActive(true);
        }
        transform.parent.transform.parent.gameObject.SetActive(false);
        value = 0f;
        mat.SetFloat("_Anxiety", 0f);
        GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);
    }

    IEnumerator InvinsibilityFrames()
    {
        invinsible = true;
        yield return new WaitForSeconds(0.2f);
        invinsible = false;
    }
}
