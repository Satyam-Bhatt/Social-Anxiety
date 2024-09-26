using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextEffect : MonoBehaviour
{
    private TMP_Text text;
    private Vector3[] verts;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }



    private void Update()
    {
        text.ForceMeshUpdate();

        TMP_TextInfo textInfo = text.textInfo;

        verts = text.mesh.vertices;

        int vertCount = 0;
        for (int i = 0; i < verts.Length; i++)
        {
            Vector3 offset = Vector3.zero;
            offset.y = Mathf.Sin((Time.time * 10 + verts[i].x * 2) / 10f) * 5;

            verts[i] += offset;

/*            verts[vertCount + 0] += new Vector3(0, Mathf.Sin(Time.time * 5f) * 13f + 13f, 0);
            verts[vertCount + 1] += new Vector3(0, Mathf.Sin(Time.time * 5f) * 13f + 13f, 0);
            verts[vertCount + 2] += new Vector3(0, Mathf.Sin(Time.time * 5f) * 13f + 13f, 0);
            verts[vertCount + 3] += new Vector3(0, Mathf.Sin(Time.time * 5f) * 13f + 13f, 0);

            vertCount += 4;*/
        }

        text.mesh.vertices = verts;
        text.canvasRenderer.SetMesh(text.mesh);

    }

/*    TMP_Text textMesh;

    Mesh mesh;

    Vector3[] vertices;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    void Update()
    {
        textMesh.ForceMeshUpdate();
        mesh = textMesh.mesh;
        vertices = mesh.vertices;

        foreach (Vector3 vertex in vertices)
        {
            Debug.Log("1: " + vertex);
        }

        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 offset = Wobble(Time.time + i);

            vertices[i] = vertices[i] + offset;
        }

        mesh.vertices = vertices;

        foreach (Vector3 vertex in mesh.vertices)
        {
            Debug.Log("2: " + vertex);
        }

        textMesh.canvasRenderer.SetMesh(mesh);
    }*/

    Vector2 Wobble(float time)
    {
        return new Vector2(Mathf.Sin(time * 3.3f), Mathf.Cos(time * 2.5f));
    }
}
