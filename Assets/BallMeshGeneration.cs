using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BallMeshGeneration : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private GameObject ball;

    private Vector3 previousPosition;

    [SerializeField] private Transform circle1, circle2;

    private void Start()
    {
        previousPosition = ball.transform.position;

        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(previousPosition, ball.transform.position) > 1f)
        {
            Vector3 direction = ball.transform.position - previousPosition;
            UpdateMesh(direction.normalized);
            previousPosition = ball.transform.position;
        }
    }

    private void UpdateMesh(Vector3 direction)
    {
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;

        Vector3 normal2D = new Vector3(0, 0, -1f);
        Vector3 pos1 = Vector3.Cross(direction, normal2D).normalized;
        Vector3 pos2 = Vector3.Cross(direction, -normal2D).normalized;

        circle1.position = pos1;
        circle2.position = pos2;

/*        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices = mesh.vertices.ToList();
        triangles = mesh.triangles.ToList();

        vertices.Add(new Vector3(x , y + 1 , 0));
        vertices.Add(new Vector3(x , y - 1 , 0));

        int vertexLength = vertices.Count - 1;

        triangles.Add(vertexLength - 3);
        triangles.Add(vertexLength - 1);
        triangles.Add(vertexLength - 2);

        triangles.Add(vertexLength - 1);
        triangles.Add(vertexLength);
        triangles.Add(vertexLength - 2);

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();*/
    }

    private void CreateMesh()
    {
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;

        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(x - 1,y + 1, 0), //0
            new Vector3(x + 1,y + 1, 0),  //1
            new Vector3(x - 1,y - 1, 0),//2
            new Vector3(x + 1,y - 1, 0)  //3
        };

        //List<Vector3> normals = new List<Vector3>();
        List<int> triangles = new List<int>
        {
            0,
            1,
            2,
            1,
            3,
            2
        };

        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.MarkDynamic();
        //mesh.SetNormals(normals);

        GetComponent<MeshFilter>().sharedMesh = mesh;
    }

    //mesh = new Mesh();
    //mesh.SetVertices(vertices);
    //    mesh.SetTriangles(triangles, 0);
    //    mesh.SetNormals(desiNormal);

    //    GetComponent<MeshFilter>().sharedMesh = mesh;
}
