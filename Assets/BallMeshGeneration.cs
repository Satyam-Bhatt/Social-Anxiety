using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class BallMeshGeneration : MonoBehaviour
{
    private Mesh mesh;

    [SerializeField] private GameObject ball;

    private Vector3 previousPosition;

    private PlayerControls playerControls;
    private EdgeCollider2D edgeCollider;
    private PolygonCollider2D polygonCollider2D;

    private bool createMesh = true;

    private void Awake()
    {
        playerControls = new PlayerControls();
        edgeCollider = GetComponent<EdgeCollider2D>();
        polygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.BallMovement.Enable();
    }

    // Start is called before the first frame update
    private void OnDisable()
    {
        playerControls.CoffeGame.BallMovement.Disable();
    }

    private void Start()
    {
        previousPosition = ball.transform.position;

        CreateMesh();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 move = playerControls.CoffeGame.BallMovement.ReadValue<Vector2>();
        Vector2 direction = new Vector2(0, 0);

        if (move == new Vector2(1, 0) || move == new Vector2(-1, 0) || move == new Vector2(0, 1) || move == new Vector2(0, -1))
        {
            direction = move;
        }

        if (Vector2.Distance(previousPosition, ball.transform.position) > 0.5f && direction != Vector2.zero)
        {
            previousPosition = ball.transform.position;
            if (createMesh)
            {
                UpdateMesh(direction.normalized);
            }
            else if (!createMesh)
            {
                DeleteMesh();
            }
        }
    }

    private void DeleteMesh()
    {
        List<Vector3> verts = new List<Vector3>();
        verts = mesh.vertices.ToList();

        verts.RemoveAt(verts.Count - 1);
        verts.RemoveAt(verts.Count - 1);

        List<int> triangles = new List<int>();
        triangles = mesh.triangles.ToList();

        for (int i = 0; i < 6; i++)
        {
            triangles.RemoveAt(triangles.Count - 1);
        }

        mesh.SetTriangles(triangles.ToArray(), 0);
        mesh.SetVertices(verts.ToArray());
        mesh.MarkDynamic();


        polygonCollider2D.points = ConvertToPolygonPoints(verts.ToArray());
    }

    private void FixedUpdate()
    {
        if (Physics2D.Raycast(ball.transform.position, Vector3.forward))
        {
            createMesh = false;
        }
        else
        {
            createMesh = true;
        }
    }

    private void OnDrawGizmos()
    {

        if (createMesh)
        {
            Handles.color = Color.green;
        }
        else
        {
            Handles.color = Color.red;
        }
        Handles.DrawWireDisc(ball.transform.position, -Vector3.forward, 1.5f, 5f);

    }

    private void UpdateMesh(Vector3 direction)
    {
        Vector3 normal2D = new Vector3(0, 0, -1f);
        Vector3 pos1 = ball.transform.position + Vector3.Cross(direction, normal2D).normalized * ball.transform.lossyScale.x / 2;
        Vector3 pos2 = ball.transform.position + Vector3.Cross(direction, -normal2D).normalized * ball.transform.lossyScale.x / 2;

        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();

        vertices = mesh.vertices.ToList();
        triangles = mesh.triangles.ToList();

        vertices.Add(pos1);
        vertices.Add(pos2);

        int vertexLength = vertices.Count - 1;

        triangles.Add(vertexLength - 3);
        triangles.Add(vertexLength - 1);
        triangles.Add(vertexLength - 2);

        triangles.Add(vertexLength - 2);
        triangles.Add(vertexLength - 1);
        triangles.Add(vertexLength);

        mesh.SetVertices(vertices.ToArray());
        mesh.SetTriangles(triangles.ToArray(), 0);

        polygonCollider2D.points = ConvertToPolygonPoints(mesh.vertices);
    }

    private void CreateMesh()
    {
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;

        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(x - ball.transform.lossyScale.x/2, y + ball.transform.lossyScale.x/2, 0), //0
            new Vector3(x - ball.transform.lossyScale.x/2, y - ball.transform.lossyScale.x/2, 0),  //1
            new Vector3(x + ball.transform.lossyScale.x/2, y + ball.transform.lossyScale.x/2, 0),//2
            new Vector3(x + ball.transform.lossyScale.x/2, y - ball.transform.lossyScale.x/2, 0)  //3
        };

        List<int> triangles = new List<int>
        {
            0,
            2,
            1,
            1,
            2,
            3
        };

        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.MarkDynamic();
        //mesh.SetNormals(normals);

        GetComponent<MeshFilter>().sharedMesh = mesh;

        polygonCollider2D.points = ConvertToPolygonPoints(mesh.vertices);
    }

    private Vector2[] ConvertToPolygonPoints(Vector3[] vertices)
    {
        List<Vector2> polygonPoints = new List<Vector2>();

        for (int i = 0; i < vertices.Length; i = i + 2)
        {
            //Debug.Log("First Loop: " + i);
            polygonPoints.Add(new Vector2(vertices[i].x, vertices[i].y));
        }
        for (int i = vertices.Length - 1; i > 0; i = i - 2)
        {
            //Debug.Log("Second Loop: " + i);

            polygonPoints.Add(new Vector2(vertices[i].x, vertices[i].y));

        }
        //Debug.Log("Polygon Points: " + polygonPoints.Count + " triangles Points: " + mesh.triangles.Length);
        return polygonPoints.ToArray();
    }

    //mesh = new Mesh();
    //mesh.SetVertices(vertices);
    //    mesh.SetTriangles(triangles, 0);
    //    mesh.SetNormals(desiNormal);

    //    GetComponent<MeshFilter>().sharedMesh = mesh;
}
