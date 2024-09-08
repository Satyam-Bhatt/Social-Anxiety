using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [Range(0,1)]
    public float t = 0;

    [SerializeField] private Transform[] points = new Transform[4];
    [SerializeField] private Transform pointSpecial;

    private float a, b;

    [SerializeField] private Sprite[] people;

    private void Start()
    {
        StartCoroutine(RandomPlacement());
    }

    // Update is called once per frame
    void Update()
    {
        t = Mathf.Cos(Time.time + Mathf.PI) * 0.5f + 0.5f;
        
        t = (a+b-2)*t*t*t + (-a-2*b+3)*t*t + b*t;

        Vector2 AB = Vector2.Lerp(points[0].position, points[1].position, t);
        Vector2 BC = Vector2.Lerp(points[1].position, points[2].position, t);
        Vector2 CD = Vector2.Lerp(points[2].position, points[3].position, t);
        
        Vector2 AB_BC = Vector2.Lerp(AB, BC, t);
        Vector2 BC_CD = Vector2.Lerp(BC, CD, t);
        Vector2 AB_BC_BC_CD = Vector2.Lerp(AB_BC, BC_CD, t);

        pointSpecial.position = AB_BC_BC_CD;
    }

    IEnumerator RandomPlacement()
    {
        while (true)
        { 
            yield return new WaitForSeconds(Mathf.PI);

            float x = Random.Range(-15f, 15f);
            float y = Random.Range(4f, 12f);

            if(t < 0.95f)
                points[1].localPosition = new Vector2(x, y);

            else
                points[1].localPosition = new Vector2(x, -y);

            x = Random.Range(-15f, 15f);
            y = Random.Range(0f, 7f);

            if(t < 0.95f)
                points[2].localPosition = new Vector2(x, y);   
            else
                points[2].localPosition = new Vector2(x, -y);

            a = Random.Range(0f, 5f);
            b = Random.Range(0f, 5f);

            pointSpecial.GetComponent<SpriteRenderer>().sprite = people[Random.Range(0, people.Length)];
        }
    }
}
