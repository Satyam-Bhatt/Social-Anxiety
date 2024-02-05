using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovementSystem : MonoBehaviour
{
    private PlayerControls playerControls;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }
    private void OnEnable()
    {
        playerControls.Movement.Move.Enable();
        playerControls.Movement.Move.performed += OnMove;
        playerControls.Movement.Interact.Enable();
        playerControls.Movement.Interact.performed += OnJump;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(InputAction.CallbackContext context)   
    {
        Debug.Log(playerControls.Movement.Move.ReadValue<Vector2>());
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump" + context);
    }
}
