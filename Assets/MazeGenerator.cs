using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject mazeCell;

    [SerializeField]
    private int rows, columns;

    private MazeCell[,] maze;

    private void Start()
    {
        maze = new MazeCell[rows, columns];

        for(int i = 0; i < rows; i++)
        {
            for(int j = 0; j < columns; j++)
            {
                GameObject newMazeCell = Instantiate(mazeCell, new Vector3(i, j, 0), Quaternion.identity);
                newMazeCell.name = "Cell (" + i + ", " + j + ")";
                maze[i, j] = newMazeCell.GetComponent<MazeCell>();
            }
        }

        //for(int i = 0; i < rows; i++)
        //{
        //    for(int j = 0; j < columns; j++)
        //    {
        //        Debug.Log("x: " + i + " y: " + j + " cell: " + maze[i, j].gameObject.name);
        //    }
        //}

        GenerateMesh(null, maze[0, 0]);
    }

    private void GenerateMesh(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();

        if (previousCell != null)
        {
            ClearWalls(previousCell, currentCell);
        }

        MazeCell nextCell;

        nextCell = GetUnvisitedNeighbor(currentCell);
    }

    private MazeCell GetUnvisitedNeighbor(MazeCell currentCell)
    {
        var unvisitedNeighbors = new List<MazeCell>();
        return null;
    }

    IEnumerable<MazeCell> GetUnvisitedNeighbors(MazeCell currentCell)
    { 
        return null;
    }

    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (currentCell.transform.position.x < previousCell.transform.position.x)
        {
            currentCell.SetWallState("right");
            previousCell.SetWallState("left");
            return;
        }
        if (currentCell.transform.position.x > previousCell.transform.position.x)
        {
            currentCell.SetWallState("left");
            previousCell.SetWallState("right");
            return;
        }
        if (currentCell.transform.position.y < previousCell.transform.position.y)
        {
            currentCell.SetWallState("bottom");
            previousCell.SetWallState("top");
            return;
        }
        if (currentCell.transform.position.y > previousCell.transform.position.y)
        {
            currentCell.SetWallState("top");
            previousCell.SetWallState("bottom");
            return;
        }
    }
}
