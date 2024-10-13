using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float distance = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x - distance, player.position.y, transform.position.z);
    }
}
