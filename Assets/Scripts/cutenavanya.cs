using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class cutenavanya : MonoBehaviour
{
    private Mesh mesh;
    
    public int radius;
    public int numPoints;

    // Start is called before the first frame update
    void Start()
    {   
        radius =1;
        numPoints = 3;
        List<Vector3> vertices = new List<Vector3>
        {
            new Vector3( 0,  0,  0), //0
        };

        List<int> triangles = new List<int>
        {
            0,
            0,
            0
        };

        mesh = new Mesh();
        mesh.SetVertices(vertices);
        mesh.SetTriangles(triangles, 0);
        mesh.MarkDynamic();
        //mesh.SetNormals(normals);

        GetComponent<MeshFilter>().sharedMesh = mesh;
        Add();
        
    }

    private void Update()
    {
        
    }

    private void Add()
    {
        Vector3[] verticesArray = mesh.vertices;
        
        List<Vector3> vertices = verticesArray.ToList();
        
        vertices.Add(new Vector3(1, 1, 0));
        mesh.SetVertices(vertices);
    }
}
