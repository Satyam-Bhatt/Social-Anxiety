using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckInOut : MonoBehaviour
{
    [SerializeField] private Transform blackEye;
    [SerializeField] private Transform outerEye;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponentInChildren<Rigidbody2D>();
    }

    private void Update()
    {        
        float x = blackEye.localPosition.x;
        float y = blackEye.localPosition.y;
        float scaleX = outerEye.localScale.x / 2 - 0.2f;
        float scaleY = outerEye.localScale.y / 2 - 0.2f;
        
        float outer = (x * x) /(scaleX * scaleX) + (y * y) / (scaleY * scaleY);

        

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction_ToMouse = (mousePosition - new Vector2(blackEye.position.x, blackEye.position.y)).normalized;

        Vector2 direction_ToConterFromMouse = (new Vector2(outerEye.position.x, outerEye.position.y) - mousePosition).normalized;
        Vector2 direction_ToCenterFromBlackEye = (new Vector2(outerEye.position.x, outerEye.position.y) - new Vector2(blackEye.position.x, blackEye.position.y)).normalized;

        float angle_Rad = Mathf.Atan2(direction_ToConterFromMouse.y, direction_ToConterFromMouse.x) + Mathf.Deg2Rad * 180;

        float lhs = (Mathf.Cos(angle_Rad) * Mathf.Cos(angle_Rad)) / (scaleX * scaleX);
        float rhs = (Mathf.Sin(angle_Rad) * Mathf.Sin(angle_Rad)) / (scaleY * scaleY);


        float r = Mathf.Sqrt(1 / (lhs + rhs));

        //Debug.Log("Cos Theta: " + Mathf.Cos(angle_Rad) + " angle: "+ angle_Rad * Mathf.Rad2Deg);

        Debug.Log(r);

        Vector2 pointOnElipse = new Vector2(r * Mathf.Cos(angle_Rad), r * Mathf.Sin(angle_Rad));
        blackEye.transform.position = new Vector3(pointOnElipse.x, pointOnElipse.y, 0);
/*
        float dotProduct = Vector2.Dot(direction_ToCenterFromBlackEye, direction_ToConterFromMouse);

        //rb.velocity = direction_ToMouse * 5f;

        if(outer < 1)
        {
            //blackEye.transform.position = Vector2.Lerp(blackEye.transform.position, mousePosition,  Time.deltaTime);
            blackEye.transform.position += new Vector3 (direction_ToMouse.x, direction_ToMouse.y, 0) * Time.deltaTime;
        }

        if (dotProduct < 0.98)
        { 
            
        }*/

    }
}
