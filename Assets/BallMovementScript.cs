using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallMovementScript : MonoBehaviour
{
    private PlayerControls playerControls;
    [SerializeField] private float magnitude;
    [SerializeField] private LayerMask mask;

    private bool up, down, right, left = false;

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
        Vector2 move = playerControls.CoffeGame.BallMovement.ReadValue<Vector2>();

        //transform.position = transform.position + new Vector3(move.x, move.y, 0) * Time.deltaTime * magnitude;

        if (move == new Vector2(1, 0) && right)
        {
            transform.position = transform.position + new Vector3(move.x, 0, 0) * Time.deltaTime * magnitude;
        }
        else if (move == new Vector2(-1, 0) && left)
        {
            transform.position = transform.position + new Vector3(move.x, 0, 0) * Time.deltaTime * magnitude;
        }
        else if (move == new Vector2(0, 1) && up)
        {
            transform.position = transform.position + new Vector3(0, move.y, 0) * Time.deltaTime * magnitude;
        }
        else if (move == new Vector2(0, -1) && down)
        {
            transform.position = transform.position + new Vector3(0, move.y, 0) * Time.deltaTime * magnitude;
        }

    }

    private void FixedUpdate()
    {
        ShootRayInAllFourDirections();
    }

    private void ShootRayInAllFourDirections()
    {
        Vector3 right1 = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y / 2, transform.position.z);
        Vector3 right2 = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
        Vector3 up1 = new Vector3(transform.position.x + transform.lossyScale.x / 2, transform.position.y, transform.position.z);
        Vector3 up2 = new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z);
        right = RayHitCheck(right1, right2, transform.right);
        left = RayHitCheck(right1, right2, -transform.right);
        up = RayHitCheck(up1, up2, transform.up);
        down = RayHitCheck(up1, up2, -transform.up);
    }

    private bool RayHitCheck(Vector3 startPos1, Vector3 startPos2, Vector3 direction)
    {

        if (Physics2D.Raycast(startPos1, direction, 1f * transform.lossyScale.x, mask) || Physics2D.Raycast(startPos2, direction, 1f * transform.lossyScale.x, mask))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    private void OnDrawGizmos()
    {
        if (right)
        {
            Handles.color = Color.green;
            Vector3 pos1 = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y / 2, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
            Handles.DrawLine(pos1, pos1 + transform.right * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 + transform.right * transform.lossyScale.x, 10f);
        }
        else if (!right)
        {
            Handles.color = Color.red;
            Vector3 pos1 = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y / 2, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
            Handles.DrawLine(pos1, pos1 + transform.right * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 + transform.right * transform.lossyScale.x, 10f);
        }

        if (left)
        {
            Handles.color = Color.green;
            Vector3 pos1 = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y / 2, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
            Handles.DrawLine(pos1, pos1 - transform.right * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 - transform.right * transform.lossyScale.x, 10f);
        }
        else if (!left)
        {
            Handles.color = Color.red;
            Vector3 pos1 = new Vector3(transform.position.x, transform.position.y + transform.lossyScale.y / 2, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x, transform.position.y - transform.lossyScale.y / 2, transform.position.z);
            Handles.DrawLine(pos1, pos1 - transform.right * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 - transform.right * transform.lossyScale.x, 10f);
        }

        if (up)
        {
            Handles.color = Color.green;
            Vector3 pos1 = new Vector3(transform.position.x + transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Handles.DrawLine(pos1, pos1 + transform.up * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 + transform.up * transform.lossyScale.x, 10f);
        }
        else if (!up)
        {
            Handles.color = Color.red;
            Vector3 pos1 = new Vector3(transform.position.x + transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Handles.DrawLine(pos1, pos1 + transform.up * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 + transform.up * transform.lossyScale.x, 10f);
        }

        if (down)
        {
            Handles.color = Color.green;
            Vector3 pos1 = new Vector3(transform.position.x + transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Handles.DrawLine(pos1, pos1 - transform.up * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 - transform.up * transform.lossyScale.x, 10f);
        }
        else if (!down)
        {
            Handles.color = Color.red;
            Vector3 pos1 = new Vector3(transform.position.x + transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Vector3 pos2 = new Vector3(transform.position.x - transform.lossyScale.x / 2, transform.position.y, transform.position.z);
            Handles.DrawLine(pos1, pos1 - transform.up * transform.lossyScale.x, 10f);
            Handles.DrawLine(pos2, pos2 - transform.up * transform.lossyScale.x, 10f);
        }
    }
}
