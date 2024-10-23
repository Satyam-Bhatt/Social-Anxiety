using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    public float distanceX, distanceY = 0f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(player.position.x - distanceX, player.position.y - distanceY, transform.position.z);
    }
}
