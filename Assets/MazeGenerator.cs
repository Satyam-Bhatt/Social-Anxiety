using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        StartCoroutine(GenerateMesh(null, maze[0, 0]));
    }

    private IEnumerator GenerateMesh(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();

        if (previousCell != null)
        {
            ClearWalls(previousCell, currentCell);
        }

        yield return new WaitForSeconds(0.1f);
        MazeCell nextCell;

        do
        {
            nextCell = GetUnvisitedNeighbor(currentCell);

            if (nextCell != null)
            { 
                Debug.Log(nextCell.transform.position);
                yield return GenerateMesh(currentCell, nextCell);
            }
        } while (nextCell != null);
    }

    private MazeCell GetUnvisitedNeighbor(MazeCell currentCell)
    {
        var unvisitedNeighbors = FindAllUnvisitedNeighbors(currentCell);

        return unvisitedNeighbors.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();
    }

    IEnumerable<MazeCell> FindAllUnvisitedNeighbors(MazeCell currentCell)
    { 
        int x = (int)currentCell.transform.position.x;
        int y = (int)currentCell.transform.position.y;

        if (x + 1 < rows)
        { 
            MazeCell rightCell = maze[x + 1, y];

            if (!rightCell.visited)
            {
                yield return rightCell;
            }
        }

        if (y + 1 < columns)
        { 
            MazeCell topCell = maze[x, y + 1];

            if (!topCell.visited)
            {
                yield return topCell;
            }
        }

        if(x - 1 >= 0)
        { 
            MazeCell leftCell = maze[x - 1, y];

            if (!leftCell.visited)
            {
                yield return leftCell;
            }
        }

        if(y - 1 >= 0)
        { 
            MazeCell mazeCell = maze[x, y - 1];

            if (!mazeCell.visited)
            {
                yield return mazeCell;
            }
        }

        //yield return null;
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
            currentCell.SetWallState("top");
            previousCell.SetWallState("bottom");
            return;
        }
        if (currentCell.transform.position.y > previousCell.transform.position.y)
        {
            currentCell.SetWallState("bottom");
            previousCell.SetWallState("top");
            return;
        }
    }
}
