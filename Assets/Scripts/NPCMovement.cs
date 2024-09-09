using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class NPCMovement : MonoBehaviour
{
    [SerializeField]
    private Transform[] movePath;

    private int movePathIndex = 1;

    [Range(0,1f)]
    [SerializeField]private float t,a,b = 0.0f;

    private float increaseValue = 0.0f;

    private void OnEnable()
    {
        StartCoroutine(RandomPlacement());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        t= 0f; a = 0f; b = 0f; increaseValue = 0f;
        transform.position = movePath[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        increaseValue += Time.deltaTime;

        t = Mathf.Cos(increaseValue + Mathf.PI) * 0.5f + 0.5f;
        t = (a + b - 2) * t * t * t + (-a - 2 * b + 3) * t * t + b * t;

        transform.position = Vector3.Lerp(movePath[0].position, movePath[1].position, t);
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

    IEnumerator RandomPlacement()
    {
        while (true)
        {
            yield return new WaitForSeconds(Mathf.PI);
            gameObject.GetComponent<SpriteRenderer>().flipX = !gameObject.GetComponent<SpriteRenderer>().flipX;
            a = Random.Range(0f, 5f);
            b = Random.Range(0f, 5f);
        }
    }
}
