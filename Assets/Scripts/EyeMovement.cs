using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeMovement : MonoBehaviour
{
    public float down_Speed = 1f;

    // Update is called once per frame
    void Update()
    {
        float y_posi = transform.position.y;
        y_posi -= down_Speed *Time.deltaTime;
        transform.position = new Vector3(transform.position.x, y_posi, transform.position.z);
    }
}
