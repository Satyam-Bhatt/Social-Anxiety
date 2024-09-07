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
        float scaleX = outerEye.localScale.x / 2 - (blackEye.localScale.x/2);
        float scaleY = outerEye.localScale.y / 2 - (blackEye.localScale.x / 2);
      
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction_ToConterFromMouse = (new Vector2(outerEye.position.x, outerEye.position.y) - mousePosition).normalized;

        float angle_Rad = Mathf.Atan2(direction_ToConterFromMouse.y, direction_ToConterFromMouse.x) + Mathf.Deg2Rad * 180;

        float lhs = (Mathf.Cos(angle_Rad) * Mathf.Cos(angle_Rad)) / (scaleX * scaleX);
        float rhs = (Mathf.Sin(angle_Rad) * Mathf.Sin(angle_Rad)) / (scaleY * scaleY);


        float r = Mathf.Sqrt(1 / (lhs + rhs));

        Vector2 pointOnElipse = new Vector2(r * Mathf.Cos(angle_Rad) + outerEye.localPosition.x, r * Mathf.Sin(angle_Rad));
        blackEye.transform.localPosition = new Vector3(pointOnElipse.x, pointOnElipse.y, 0);

    }
}
