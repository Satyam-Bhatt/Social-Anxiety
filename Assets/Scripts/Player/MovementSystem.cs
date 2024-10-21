using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using Unity.VisualScripting;
using Cinemachine;

public class MovementSystem : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private GameObject ObjectToPickUp;

    private Vector2 moveInput = Vector2.zero;
    private bool canInteract = false;
    private bool canTravel = false;
    private bool messageShown = false;
    public bool cutscenePlaying = false;
    private bool coffeeGamePlaying = false;
    private bool canSleep = false;

    private IEnumerator coroutine;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private Eye_Player eyePlayer;

    [SerializeField]
    private GameObject kitchen;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject outside;
    [SerializeField]
    private GameObject park;
    [SerializeField]
    private GameObject sleepPanel;

    [SerializeField]
    private PlayableDirector timeline;

    [SerializeField]
    private PlayableDirector sleepTimeline;

    [Header("Canvas")]
    [Space(10)]

    [SerializeField]
    private GameObject interactPanel;
    [SerializeField]
    private GameObject notePanel;
    [SerializeField]
    private GameObject coffeeGamePanel;

    public Material[] materials = new Material[2];

    private TMP_Text interactText;
    private TMP_Text noteText;
    private TMP_Text noteHead;
    private Notes noteScript;

    private string doorName;

    private Animator animator;

    public delegate void Timeline_Start();
    public event Timeline_Start onTimelineStart;

    private CoffeeGame coffeeGame;
    private RandomThoughts randomThoughts;

    [SerializeField] private CinemachineVirtualCamera vcam;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coffeeGame = GetComponent<CoffeeGame>();
        randomThoughts = GameManager.Instance.GetComponent<RandomThoughts>();
    }
    private void OnEnable()
    {
        playerControls.Movement.Move.Enable();

        playerControls.Movement.Interact.Enable();
        playerControls.Movement.Interact.performed += Interact;

        timeline.stopped += OnPlayableDirectorStopped;
        sleepTimeline.stopped += OnSleepTimelineStateChanged;

        coffeeGame.onCoffeeGameCompleted += OnCoffeeGameComplete;
    }

    private void OnDisable()
    {
        playerControls.Movement.Move.Disable();

        playerControls.Movement.Interact.Disable();
        playerControls.Movement.Interact.performed -= Interact;

        timeline.stopped -= OnPlayableDirectorStopped;
        sleepTimeline.stopped -= OnSleepTimelineStateChanged;

        coffeeGame.onCoffeeGameCompleted -= OnCoffeeGameComplete;
    }

    // Start is called before the first frame update
    void Start()
    {
        room.SetActive(true);
        kitchen.SetActive(false);
        outside.SetActive(false);
        park.SetActive(false);
        coffeeGamePanel.SetActive(false);
        transform.position = Vector3.zero;

        interactPanel.SetActive(false);
        interactText = interactPanel.GetComponentInChildren<TMP_Text>();

        notePanel.SetActive(false);
        noteText = notePanel.transform.GetChild(1).GetComponent<TMP_Text>();
        noteHead = notePanel.transform.GetChild(0).GetComponent<TMP_Text>();

        coroutine = MovePlayer();

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        room.transform.GetChild(1).gameObject.SetActive(false);
        coffeeGame.enabled = false;

        room.transform.Find("Knife").gameObject.SetActive(false);

    }

    private bool moveStopper = false;

    void Update()
    {
        if (cutscenePlaying) return;

        if (!moveStopper)
        {
            moveInput = playerControls.Movement.Move.ReadValue<Vector2>().normalized;
        }
        else
        {
            moveInput = Vector2.zero;
            CheckInput();
        }

        //-----ANIMATION-----//
        animator.SetFloat("BlendX", moveInput.x);
        animator.SetFloat("BlendY", moveInput.y);

        if (canInteract)
        {
            interactPanel.SetActive(true);
            interactText.text = "Press E to pickup item";
        }
        else if (noteScript != null)
        {
            interactPanel.SetActive(!messageShown);
            interactText.text = "Press E to Interact";
        }
        else if (canSleep)
        {
            interactPanel.SetActive(true);
            interactText.text = "Press E to sleep";
        }
    }

    private bool takeInput = false;
    private void CheckInput()
    {
        if (takeInput)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D))
            {
                moveStopper = false;
                takeInput = false;
                animator.SetBool("Confused", false);
            }
        }
    }

    IEnumerator StopMovement()
    {
        while (true)
        {
            float delay = Random.Range(5f, 12f);
            yield return new WaitForSeconds(delay);
            moveStopper = true;
            animator.SetBool("Confused", true);
            yield return new WaitForSeconds(1.5f);
            takeInput = true;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cutscenePlaying) return;

        rb.velocity = moveInput * moveSpeed;

        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D collider in colliderArray)
        {
            if (collider.gameObject.tag != "Player")
            {
                if (collider.GetComponent<Object>() != null)
                {
                    ObjectToPickUp = collider.gameObject;
                    canInteract = true;
                }
                else if (collider.gameObject.layer == 7)
                {
                    canTravel = true;
                    doorName = collider.gameObject.tag;
                }
                else if (collider.gameObject.layer == 9)
                {
                    noteScript = collider.GetComponent<Notes>();
                }
                else if (collider.gameObject.layer == 10)
                {
                    gameObject.GetComponent<CoffeeGame>().enabled = true;
                    MazeGenerator.Instance.arrow.SetActive(false);
                    MazeGenerator.Instance.t1.SetActive(false);
                    MazeGenerator.Instance.t2.SetActive(false);
                    MazeGenerator.Instance.t1.transform.parent.GetChild(0).gameObject.SetActive(false);
                    if (eyePlayer.position)
                    {
                        MazeGenerator.Instance.ActiveDeactivateMaze(true);

                        gameObject.GetComponent<CoffeeGame>().canPlay = true;
                        gameObject.GetComponent<CoffeeGame>().image.gameObject.SetActive(true);
                    }
                    else
                    {
                        MazeGenerator.Instance.ActiveDeactivateMaze(false);

                        gameObject.GetComponent<CoffeeGame>().canPlay = false;
                        gameObject.GetComponent<CoffeeGame>().image.gameObject.SetActive(false);
                    }
                }
                else if (collider.gameObject.layer == 11)
                {
                    canSleep = true;
                }
            }
        }
        if (ArrayChecker(colliderArray) == 0)
        {
            ObjectToPickUp = null;
            doorName = null;
            canInteract = false;
            canTravel = false;
            interactPanel.SetActive(false);
            notePanel.SetActive(false);

            canSleep = false;

            if (noteScript != null)
            {
                if (!GameManager.Instance.isBW && messageShown)
                {
                    //GameManager.Instance.AudioPlay(noteScript.audio_BeforeBW);
                    randomThoughts.ClipPlay_Immediate(noteScript.auddioCaption[0]);
                }
                else if (GameManager.Instance.isBW && messageShown)
                {
                    //GameManager.Instance.AudioPlay(noteScript.audio_AfterBW);
                    randomThoughts.ClipPlay_Immediate(noteScript.auddioCaption[1]);
                }
                messageShown = false;
                noteScript = null;
            }

            //Turnig off Coffee Game
            gameObject.GetComponent<CoffeeGame>().canPlay = false;
            gameObject.GetComponent<CoffeeGame>().image.gameObject.SetActive(false);
            MazeGenerator.Instance.ActiveDeactivateMaze(false);
            if (coffeeGameActive)
            {
                MazeGenerator.Instance.PointArrow();
            }
        }
    }

    private int ArrayChecker(Collider2D[] collider2Ds)
    {
        int i = 1;

        foreach (Collider2D c in collider2Ds)
        {
            if (c.gameObject.layer == 3 || c.gameObject.layer == 0)
            {
                i = 0;
            }
            else
            {
                i = 1;
                break;
            }
        }

        return i;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("ParkCollider"))
        {
            park.SetActive(true);
            outside.SetActive(false);
            transform.position = new Vector3(10.69f, -49.5f, 0f);
        }
        else if (collision.CompareTag("Park_OutsideCollider"))
        {
            outside.SetActive(true);
            park.SetActive(false);
            transform.position = new Vector3(10.69f, -42.32f, 0f);
        }

        if (collision.gameObject.layer == 7)
        {
            interactPanel.SetActive(true);
            interactText.text = "Press E to open door";
        }

        if (collision.CompareTag("Trigger"))
        {
            AudioManager.Instance.GetComponent<AudioSource>().volume = 0.2f;
            timeline.Play();
            GameManager.Instance.tasks.transform.parent.gameObject.SetActive(false);
            onTimelineStart.Invoke();
            cutscenePlaying = true;
            rb.velocity = Vector3.zero;
            animator.SetBool("Cutscene", true);
            /*            animator.SetFloat("BlendX", -1);
                        animator.SetFloat("BlendY", 0);*/
            StartCoroutine(coroutine);

            Destroy(collision.gameObject);
        }

        if (collision.CompareTag("CoffeeGame"))
        {
            coffeeGamePanel.SetActive(true);
            interactText.text = "Press E to start game";
            coffeeGamePlaying = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactPanel.SetActive(false);

        if (collision.CompareTag("CoffeeGame"))
        {
            coffeeGamePanel.SetActive(false);
            coffeeGamePlaying = false;
        }
    }

    /*    private void OnDrawGizmos()
        {
            Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), 1.5f);
        }*/

    private bool coffeeGameActive = false;

    public void Interact(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            Object newObject = ObjectToPickUp.GetComponent<Object>();
            inventoryManager.CheckList(newObject.itemID);
            Destroy(ObjectToPickUp);
            ObjectToPickUp = null;
        }
        else if (canTravel)
        {
            if (doorName == "RoomDoor")
            {
                kitchen.SetActive(true);
                room.SetActive(false);
                transform.position = new Vector3(5.14f, -13.03f, 0f);
            }
            else if (doorName == "KitchenRoomDoor")
            {
                room.SetActive(true);
                kitchen.SetActive(false);
                transform.position = new Vector3(5.14f, -6.89f, 0f);
                if (GameManager.Instance.isBW && GameManager.Instance.coffeeGameDone)
                {
                    room.transform.GetChild(0).gameObject.SetActive(false);
                    room.transform.GetChild(1).gameObject.SetActive(true);

                    if (randomThoughts.audioCaption[8].clip != null)
                    {
                        randomThoughts.ClipPlay_Delay(8, 1f);
                        randomThoughts.audioCaption[8].clip = null;
                    }
                }
            }
            else if (doorName == "KitchenOutsideDoor")
            {
                outside.SetActive(true);
                kitchen.SetActive(false);
                transform.position = new Vector3(10.46f, -37.46f, 0f);

                if (randomThoughts.audioCaption[2].clip != null)
                {
                    randomThoughts.ClipPlay_Immediate(2);
                    randomThoughts.audioCaption[2].clip = null;
                }
            }
            else if (doorName == "OutsideDoor")
            {
                if (!GameManager.Instance.isBW)
                {
                    kitchen.SetActive(true);
                    outside.SetActive(false);
                    transform.position = new Vector3(10.46f, -31.0f, 0f);
                }

                if (GameManager.Instance.isBW)
                {
                    outside.GetComponent<ConfirmationPopUp>().PanelActivate();
                }
            }

        }
        else if (noteScript != null)
        {
            noteScript.gameObject.GetComponent<SpriteRenderer>().material = materials[1];

            if (noteScript.type == Notes.Type.Notes)
            {
                notePanel.SetActive(true);
                messageShown = true;
                noteHead.text = noteScript.heading;

                if (!GameManager.Instance.isBW)
                {
                    noteText.text = noteScript.beforeBW;
                }
                else
                {
                    noteText.text = noteScript.afterBW;
                }
            }
            else
            {
                if (!GameManager.Instance.isBW)
                {
                    Debug.Log("cc1");
                    //GameManager.Instance.AudioPlay(noteScript.audio_BeforeBW);
                    randomThoughts.ClipPlay_Immediate(noteScript.auddioCaption[0]);
                }
                else if (GameManager.Instance.isBW)
                {
                    Debug.Log("cc2");
                    //GameManager.Instance.AudioPlay(noteScript.audio_AfterBW);
                    randomThoughts.ClipPlay_Immediate(noteScript.auddioCaption[1]);
                }
            }

            if (!noteScript.confidneceIncreased)
            {
                InventoryManager.Instance.ConfidenceIncrease();
                noteScript.confidneceIncreased = true;
            }
        }
        else if (coffeeGamePlaying)
        {
            coffeeGameActive = true;
            interactPanel.SetActive(false);
            GameManager.Instance.tasks.transform.parent.gameObject.SetActive(false);
            TransitionManager.Instance.coffeeGame.transform.GetChild(0).gameObject.SetActive(false);

            var transposer_ = vcam.GetCinemachineComponent<CinemachineTransposer>();
            if (transposer_.m_FollowOffset.x < 4.8f)
            {
                StartCoroutine(CameraOffset());
            }
        }
        else if (canSleep)
        {
            sleepTimeline.Play();
            GameManager.Instance.audioSrc.Stop();
            GameManager.Instance.tasks.transform.parent.gameObject.SetActive(false);
            room.transform.GetChild(1).gameObject.SetActive(false);
            cutscenePlaying = true;
        }
    }

    private IEnumerator CameraOffset()
    {
        float x = 0;
        while (x < 4.6f)
        {
            x = Mathf.Lerp(x, 5f, 10f * Time.deltaTime);
            var transposer_ = vcam.GetCinemachineComponent<CinemachineTransposer>();
            transposer_.m_FollowOffset = new Vector3(x, transposer_.m_FollowOffset.y, transposer_.m_FollowOffset.z);
            yield return new WaitForEndOfFrame();
        }
        TransitionManager.Instance.coffeeGame.transform.GetChild(1).gameObject.SetActive(true);
        TransitionManager.Instance.coffeeGame.transform.GetChild(2).gameObject.SetActive(true);
        AudioManager.Instance.AudioPlay(AudioManager.Instance.coffeeGame_Audio);
        gameObject.GetComponent<CoffeeGame>().enabled = true;

        for (int i = 0; i < MazeGenerator.Instance.transform.childCount; i++)
        {
            MazeGenerator.Instance.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    [SerializeField]
    private Transform unknownGuy;

    private IEnumerator MovePlayer()
    {
        while (transform.position != new Vector3(-5.4f, -49.9f, 0f))
        {
            if (timeline.time > 3.7f && timeline.time < 3.8f)
                timeline.Pause();
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-5.4f, -49.9f, 0f), 1f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }


        while (unknownGuy.transform.position != new Vector3(-7.0f, -49.9f, 0f))
        {
            if (timeline.time > 3.7f && timeline.time < 3.8f)
                timeline.Pause();
            unknownGuy.transform.position = Vector3.MoveTowards(unknownGuy.transform.position, new Vector3(-7.0f, -49.9f, 0f), 1.2f * Time.deltaTime);
            yield return new WaitForEndOfFrame();
        }

        timeline.Play();
        yield return null;

    }

    void OnPlayableDirectorStopped(PlayableDirector timeline)
    {
        cutscenePlaying = false;
        StopCoroutine(coroutine);

        StartCoroutine(EnableChild(0));

        GameManager.Instance.gameObject.GetComponent<RandomThoughts>().ClipPlay_Delay(0, 2f);
        // Use coroutine instead
        float delay = GameManager.Instance.gameObject.GetComponent<RandomThoughts>().audioCaption[0].clip.length;
        StartCoroutine(AudioPlay_Delay(1, delay + 2f + 1f));
        float delay2 = randomThoughts.audioCaption[1].clip.length + delay + 1f;
        StartCoroutine(AudioPlay_Delay(6, delay2 + 3f));

        unknownGuy.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        unknownGuy.gameObject.GetComponent<NPCMovement>().enabled = true;

        GameManager.Instance.tasks.text = "- Make Coffee";
        GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);

        //Changing all the notes to be different material and contain different Information
        Notes[] notes = FindObjectsOfType<Notes>(true);

        foreach (Notes n in notes)
        {
            GameObject g = n.gameObject;
            g.GetComponent<SpriteRenderer>().material = materials[0];
        }

        AudioManager.Instance.GetComponent<AudioSource>().volume = 0.5f;
        animator.SetBool("BW", true);
        animator.SetBool("Cutscene", false);

        moveSpeed = 3;
        StartCoroutine(StopMovement());
    }

    IEnumerator EnableChild(int index)
    {
        while (index < transform.childCount)
        {
            transform.GetChild(index).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            index++;
        }
    }

    public void OnSleepTimelineStateChanged(PlayableDirector timeline)
    {
        cutscenePlaying = false;
        sleepPanel.SetActive(false);
        StartCoroutine(DisableChild(0));
        room.transform.GetChild(2).gameObject.SetActive(false);
        GameManager.Instance.tasks.text = "- Escape";
        GameManager.Instance.tasks.transform.parent.gameObject.SetActive(true);

        for (int i = 0; i < room.transform.childCount; i++)
        {
            room.transform.GetChild(i).gameObject.SetActive(false);
        }

        room.transform.Find("Knife").gameObject.SetActive(true);

        randomThoughts.ClipPlay_Delay(7, 2f);
    }

    IEnumerator DisableChild(int index)
    {
        while (index < transform.childCount)
        {
            transform.GetChild(index).gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            index++;
        }
    }

    public void ConfirmationPopUp_Yes()
    {
        kitchen.SetActive(true);
        outside.SetActive(false);
        transform.position = new Vector3(10.46f, -31.0f, 0f);
        outside.transform.GetChild(0).gameObject.SetActive(false);
        kitchen.transform.GetChild(1).gameObject.SetActive(false);
    }

    public void OnCoffeeGameComplete()
    {
        coffeeGameActive = false;
        randomThoughts.ClipPlay_Immediate(13);
        float delay = randomThoughts.audioCaption[13].clip.length;
        randomThoughts.ClipPlay_Delay(14, delay + 1f);

        AudioManager.Instance.AudioPlay(AudioManager.Instance.afterBW_Clip);
        for (int i = 0; i < MazeGenerator.Instance.transform.childCount; i++)
        {
            MazeGenerator.Instance.transform.GetChild(i).gameObject.SetActive(false);
        }

        StartCoroutine(CameraOffsetReset());
    }

    private IEnumerator CameraOffsetReset()
    {
        var transposer_ref = vcam.GetCinemachineComponent<CinemachineTransposer>();
        float x = transposer_ref.m_FollowOffset.x;
        while (x > 0.01f)
        {
            x = Mathf.Lerp(x, 0f, 3f * Time.deltaTime);

            var transposer = vcam.GetCinemachineComponent<CinemachineTransposer>();
            transposer.m_FollowOffset = new Vector3(x, transposer.m_FollowOffset.y, transposer.m_FollowOffset.z);
            yield return new WaitForEndOfFrame();
        }
        var transposer_ = vcam.GetCinemachineComponent<CinemachineTransposer>();
        transposer_.m_FollowOffset = new Vector3(0, transposer_.m_FollowOffset.y, transposer_.m_FollowOffset.z);
    }

    IEnumerator AudioPlay_Delay(int clipIndex, float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.gameObject.GetComponent<RandomThoughts>().ClipPlay_Immediate(clipIndex);
    }

}
