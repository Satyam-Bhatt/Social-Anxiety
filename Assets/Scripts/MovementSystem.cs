using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    private PlayerControls playerControls;
    private Rigidbody2D rb;

    private Vector2 moveInput = Vector2.zero;

    [SerializeField]
    private float moveSpeed = 10f;

    private void Awake()
    {
        playerControls = new PlayerControls();
        rb = GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        playerControls.Movement.Move.Enable();

        playerControls.Movement.Interact.Enable();
        playerControls.Movement.Interact.performed += OnJump;
    }

    private void OnDisable()
    {
        playerControls.Movement.Move.Disable();

        playerControls.Movement.Interact.Disable();
        playerControls.Movement.Interact.performed -= OnJump;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        moveInput = playerControls.Movement.Move.ReadValue<Vector2>();
        rb.velocity = moveInput * moveSpeed;
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump" + context);
    }
}
