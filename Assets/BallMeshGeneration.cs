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

        if (Vector2.Distance(previousPosition, ball.transform.position) > 2f && direction != Vector2.zero)
        {
            previousPosition = ball.transform.position;
            UpdateMesh(direction.normalized);
        }
    }

    private void FixedUpdate()
    {
        if (Physics2D.CircleCast(ball.transform.position, ball.transform.lossyScale.x/20 , Vector3.forward))
        { 
/*            RaycastHit2D hit = Physics2D.Raycast(ball.transform.position, Vector3.forward, 1f);
            Debug.Log(hit.collider.name);*/
            createMesh = false;
        }
        else
        {
            createMesh = true;
        }
    }

    private void OnDrawGizmos()
    {
        Handles.color = Color.magenta;
        Handles.DrawWireDisc(ball.transform.position, -Vector3.forward, ball.transform.lossyScale.x/20, 5f);

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
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;

        Vector3 normal2D = new Vector3(0, 0, -1f);
        Vector3 pos1 = ball.transform.position + Vector3.Cross(direction, normal2D).normalized;
        Vector3 pos2 = ball.transform.position + Vector3.Cross(direction, -normal2D).normalized;

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

        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();

        //edgeCollider.points = ConvertToPolygonPoints(mesh.vertices);
        Debug.Log("Call: " + mesh.vertices.Length);
        polygonCollider2D.points = ConvertToPolygonPoints(mesh.vertices);
    }



    private void CreateMesh()
    {
        float x = ball.transform.position.x;
        float y = ball.transform.position.y;

        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3(x - 1,y + 1, 0), //0
            new Vector3(x - 1,y - 1, 0),  //1
            new Vector3(x + 1,y + 1, 0),//2
            new Vector3(x + 1,y - 1, 0)  //3
        };

        //List<Vector3> normals = new List<Vector3>();
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

        //edgeCollider.points = ConvertToPolygonPoints(mesh.vertices);
        polygonCollider2D.points = ConvertToPolygonPoints(mesh.vertices);
    }

    private Vector2[] ConvertToPolygonPoints(Vector3[] vertices)
    {
        Vector2[] polygonPoints = new Vector2[vertices.Length];
        Debug.Log(vertices.Length);

        for (int j = 0; j < vertices.Length; j=j+4)
        { 
                polygonPoints[j + 0] = vertices[j + 1];
                polygonPoints[j + 1] = vertices[j + 0];
            if(j+2 < vertices.Length)
                polygonPoints[j + 2] = vertices[j + 2];
            if(j+3 < vertices.Length)
                polygonPoints[j + 3] = vertices[j + 3];
        }

        return polygonPoints;


        //for (int i = 0; i < vertices.Length; i++)
        //{
        //    polygonPoints[i] = new Vector2(vertices[i].x, vertices[i].y);
        //}
        //return polygonPoints;
    }

    //mesh = new Mesh();
    //mesh.SetVertices(vertices);
    //    mesh.SetTriangles(triangles, 0);
    //    mesh.SetNormals(desiNormal);

    //    GetComponent<MeshFilter>().sharedMesh = mesh;
}
