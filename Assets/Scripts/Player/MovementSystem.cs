using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Playables;
using Unity.VisualScripting;

public class MovementSystem : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private GameObject ObjectToPickUp;

    private Vector2 moveInput = Vector2.zero;
    private bool canInteract = false;
    private bool canTravel = false;
    private bool messageShown = false;
    private bool cutscenePlaying = false;

    private IEnumerator coroutine;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private GameObject kitchen;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject outside;
    [SerializeField]
    private GameObject park;

    [SerializeField]
    private PlayableDirector timeline;

    [Header("Canvas")]
    [Space(10)]

    [SerializeField]
    private GameObject interactPanel;
    [SerializeField]
    private GameObject notePanel;

    [TextArea(4, 2)]
    [SerializeField]
    private string[] notes;

    private TMP_Text interactText;
    private TMP_Text noteText;
    private Notes noteScript;

    private string doorName;

    [SerializeField]
    private GameObject panel;//debug code

    private Animator animator;

    public delegate void Timeline_Start();
    public event Timeline_Start onTimelineStart;

    private CoffeeGame coffeeGame;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coffeeGame = GetComponent<CoffeeGame>();
    }
    private void OnEnable()
    {
        playerControls.Movement.Move.Enable();

        playerControls.Movement.Interact.Enable();
        playerControls.Movement.Interact.performed += Interact;

        timeline.stopped += OnPlayableDirectorStopped;
    }

    private void OnDisable()
    {
        playerControls.Movement.Move.Disable();

        playerControls.Movement.Interact.Disable();
        playerControls.Movement.Interact.performed -= Interact;

        timeline.stopped -= OnPlayableDirectorStopped;
    }

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);//debug code
        room.SetActive(true);
        kitchen.SetActive(false);
        outside.SetActive(false);
        park.SetActive(false);
        transform.position = Vector3.zero;

        interactPanel.SetActive(false);
        interactText = interactPanel.GetComponentInChildren<TMP_Text>();

        notePanel.SetActive(false);
        noteText = notePanel.GetComponentInChildren<TMP_Text>();

        coroutine = MovePlayer();

        for(int i=0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        coffeeGame.enabled = false;
    }

    void Update()
    {
        if(cutscenePlaying) return;
        
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>().normalized;

        //-----ANIMATION-----//
        if (moveInput.x > 0)
        {
            animator.SetInteger("MovementSwitch", 2);
        }
        else if (moveInput.x < 0)
        {
            animator.SetInteger("MovementSwitch", 3);
        }
        else if (moveInput.y > 0)
        {
            animator.SetInteger("MovementSwitch", 1);
        }
        else if (moveInput.y < 0)
        {
            animator.SetInteger("MovementSwitch", 0);
        }

        if (canInteract)
        {
            interactPanel.SetActive(true);
            interactText.text = "Press E to pickup item";
        }
        else if (noteScript != null && noteScript.noteNumber != 0)
        {
            interactPanel.SetActive(!messageShown);
            interactText.text = "Press E to read the note";
        }

        //-------Debug Timeline-------//
        if (Input.GetKeyDown(KeyCode.T))
        {
            timeline.Play();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartCoroutine(EnableChild(0));
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (cutscenePlaying) return;

        rb.velocity = moveInput * moveSpeed;

        Collider2D[] colliderArray = Physics2D.OverlapCircleAll(transform.position, 2f);
        foreach(Collider2D collider in colliderArray)
        {
            if (collider.gameObject.tag != "Player") {
                if (collider.GetComponent<Object>() != null)
                {
                    ObjectToPickUp = collider.gameObject;
                    panel.SetActive(true);//debug code
                    canInteract = true;
                }
                else if (collider.gameObject.layer == 7)
                {
                    canTravel = true;
                    doorName = collider.gameObject.tag;
                    panel.SetActive(true);//debug code
                }
                else if (collider.gameObject.layer == 9)
                {
                    noteScript = collider.GetComponent<Notes>();
                }
                else if (collider.gameObject.layer == 10)
                {
                    this.gameObject.GetComponent<CoffeeGame>().enabled = true;
                }
            }
        }
        if (ArrayChecker(colliderArray) == 0)
        {
            ObjectToPickUp = null;
            doorName = null;
            panel.SetActive(false);//debug code
            canInteract = false;
            canTravel = false;
            interactPanel.SetActive(false);
            notePanel.SetActive(false);
            messageShown = false;
            noteScript = null;
            this.gameObject.GetComponent<CoffeeGame>().enabled = false;
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
        else if(collision.CompareTag("Park_OutsideCollider"))
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
            timeline.Play();
            onTimelineStart.Invoke();
            cutscenePlaying = true;
            rb.velocity = Vector3.zero;
            animator.SetInteger("MovementSwitch", 3);
            StartCoroutine(coroutine);
            Destroy(collision.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        interactPanel.SetActive(false);
    }

    private void OnDrawGizmos()
    {
        Handles.DrawWireDisc(transform.position, new Vector3(0, 0, 1), 2f);
    }

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
            }
            else if (doorName == "KitchenOutsideDoor")
            {
                outside.SetActive(true);
                kitchen.SetActive(false);
                animator.SetInteger("MovementSwitch", 0);
                transform.position = new Vector3(10.46f, -37.46f, 0f);
            }
            else if (doorName == "OutsideDoor")
            {
                kitchen.SetActive(true);
                outside.SetActive(false);
                transform.position = new Vector3(10.46f, -31.0f, 0f);
            }

        }
        else if(noteScript != null && noteScript.noteNumber != 0)
        {
            notePanel.SetActive(true);
            messageShown = true;
            
            if (noteScript.noteNumber == 1)
            {
                noteText.text = notes[noteScript.noteNumber - 1];
                if (!noteScript.confidneceIncreased)
                {
                    InventoryManager.Instance.ConfidenceIncrease();
                    noteScript.confidneceIncreased = true;
                }
            }
            else if(noteScript.noteNumber == 2)
            {
                noteText.text = notes[noteScript.noteNumber - 1];
                if (!noteScript.confidneceIncreased)
                {
                    InventoryManager.Instance.ConfidenceIncrease();
                    noteScript.confidneceIncreased = true;
                }
            }
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

        unknownGuy.gameObject.GetComponent<SpriteRenderer>().flipX = true;
        unknownGuy.gameObject.GetComponent<NPCMovement>().enabled = true;
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

    
}
