using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestS : MonoBehaviour
{
    [SerializeField] private Transform rect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float rightX = rect.transform.position.x + rect.transform.lossyScale.x / 2;
        float leftX = rect.transform.position.x - rect.transform.lossyScale.x / 2;
        float topY = rect.transform.position.y + rect.transform.lossyScale.y / 2;
        float bottomY = rect.transform.position.y - rect.transform.lossyScale.y / 2;

        if (transform.position.x < rightX && transform.position.x > leftX && transform.position.y < topY && transform.position.y > bottomY)
        {
            Debug.Log("In");
        }
    }
}
