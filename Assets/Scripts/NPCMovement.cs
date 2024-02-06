using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePath;

    private int movePathIndex = 0;

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = movePath[movePathIndex].position - transform.position;
        
        transform.Translate(direction * 1 * Time.deltaTime, Space.Self);

        //For linear motion
        //transform.position = Vector3.MoveTowards(transform.position, movePath[movePathIndex].position, 1 * Time.unscaledDeltaTime);

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
}
