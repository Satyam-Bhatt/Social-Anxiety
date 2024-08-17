using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor;
using UnityEngine.UI;

public class MovementSystem : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;
    private GameObject ObjectToPickUp;

    private Vector2 moveInput = Vector2.zero;
    private bool canInteract = false;
    private bool canTravel = false;

    [SerializeField]
    private float moveSpeed = 10f;
    [SerializeField]
    private InventoryManager inventoryManager;

    [SerializeField]
    private GameObject kitchen;
    [SerializeField]
    private GameObject room;

    private string doorName;

    [SerializeField]
    private GameObject panel;//debug code

    private Animator animator;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        playerControls.Movement.Move.Enable();

        playerControls.Movement.Interact.Enable();
        playerControls.Movement.Interact.performed += Interact;
    }

    private void OnDisable()
    {
        playerControls.Movement.Move.Disable();

        playerControls.Movement.Interact.Disable();
        playerControls.Movement.Interact.performed -= Interact;
    }

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);//debug code
        kitchen.SetActive(false);
    }

    void Update()
    {
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();

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
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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
                Debug.Log(collider.gameObject.tag);
            }
        }
        if (colliderArray.Length == 1)
        {
            ObjectToPickUp = null;
            doorName = null;
            panel.SetActive(false);//debug code
            canInteract = false;
            canTravel = false;
        }
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
            if (doorName == "RoomDoor") {
                kitchen.SetActive(true);
                room.SetActive(false);
                transform.position = new Vector3(2.49f, -9.58f, 0f);
            }
            else if (doorName == "KitchenRoomDoor")
            {
                room.SetActive(true);
                kitchen.SetActive(false);
                transform.position = new Vector3(-6.47f, -6.29f, 0f);
            }

        }
    }
}
