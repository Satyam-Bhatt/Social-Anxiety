using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Eye_Player : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private Sprite[] EyesClose_Open = new Sprite[2];
    private SpriteRenderer s;

    private bool position = false;

    private void Awake()
    {
        playerControls = new PlayerControls();
        s = gameObject.transform.GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.EyesClose.Enable();
        playerControls.CoffeGame.EyesClose.started += SpriteChange;
        playerControls.CoffeGame.EyesClose.canceled += SpriteChange;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.EyesClose.Disable();
        playerControls.CoffeGame.EyesClose.started -= SpriteChange;
        playerControls.CoffeGame.EyesClose.canceled -= SpriteChange;
    }

    private void Start()
    {
        s.sprite = EyesClose_Open[1];
    }

    private void Update()
    {
        if (position)
        { 
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = Vector3.Lerp(transform.position, mousePosition, 30 * Time.deltaTime);
        }
    }

    public void SpriteChange(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            s.sprite = EyesClose_Open[1];
            position = true;
        }
        else 
        {
            s.sprite = EyesClose_Open[0];
            position = false;        
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}