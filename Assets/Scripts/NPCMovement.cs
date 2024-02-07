using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePath;

    private int movePathIndex = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = (movePath[movePathIndex].position - transform.position).normalized;
        
        transform.Translate(direction * 1 * Time.deltaTime, Space.World);

        //For linear motion
        //transform.position = Vector3.MoveTowards(transform.position, movePath[movePathIndex].position, 1 * Time.deltaTime);

        transform.rotation = Quaternion.AngleAxis(GetAngleFromVectorFloat(direction), Vector3.forward);

        if (Vector3.Distance(movePath[movePathIndex].position, transform.position) < 0.1f)
        {
            if (movePathIndex < movePath.Length - 1)
            {
                movePathIndex++;
            }
            else 
            {
                movePathIndex = 0;
            }
        }
    }

    private float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0)
        {
            n += 360;
        }
        return n;
    }
}
