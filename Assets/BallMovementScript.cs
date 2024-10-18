using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private float magnitude;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.BallMovement.Enable();
    }

    // Start is called before the first frame update
    private void OnDisable()
    {
        playerControls.CoffeGame.BallMovement.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerControls.CoffeGame.BallMovement.ReadValue<Vector2>() * magnitude;
       
        transform.position = transform.position + new Vector3(move.x, move.y, 0) * Time.deltaTime;
    }
}
