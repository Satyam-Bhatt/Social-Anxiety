using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MazeGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject mazeCell, mazeParent, mazeBall, mazeGoal;

    [SerializeField]
    private int rows, columns;


    private MazeCell[,] maze;

    private bool done, ballSpawned = false;

    private void Start()
    {
        maze = new MazeCell[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                GameObject newMazeCell = Instantiate(mazeCell, new Vector3(mazeParent.transform.position.x + i, mazeParent.transform.position.y + j, 0), Quaternion.identity);
                newMazeCell.name = "Cell (" + i + ", " + j + ")";
                newMazeCell.transform.SetParent(mazeParent.transform);
                maze[i, j] = newMazeCell.GetComponent<MazeCell>();
            }
        }

        StartCoroutine(GenerateMesh(null, maze[0, 0]));

    }

    MazeCell nextCellStore = null;

    private IEnumerator GenerateMesh(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();

        if (previousCell != null)
        {
            ClearWalls(previousCell, currentCell);
        }

        yield return new WaitForSeconds(0.05f);
        MazeCell nextCell;

        do
        {
            nextCell = GetUnvisitedNeighbor(currentCell);

            if (nextCell != null)
            {
                yield return GenerateMesh(currentCell, nextCell);
                nextCellStore = nextCell;
            }
        } while (nextCell != null);

        foreach (MazeCell cell in maze)
        {
            if (cell.visited == false)
            {
                done = false;
                break;
            }
            else
            {
                done = true;
            }
        }

        if (done && !ballSpawned)
        {
            GameObject mazeBall_Get = Instantiate(mazeBall, new Vector3(mazeParent.transform.position.x, mazeParent.transform.position.y, 0), Quaternion.identity);
            mazeBall_Get.transform.SetParent(mazeParent.transform);

            //Set Maze ball size here
            float scaleX = mazeBall_Get.transform.localScale.x;
            float scaleY = mazeBall_Get.transform.localScale.y;
            mazeBall_Get.transform.localScale = new Vector3(scaleX - 1  , scaleY - 1, 1f);

            //Instantiate Goal
            //GameObject mazeGoal_Get = Instantiate(mazeGoal, maze[UnityEngine.Random.Range(0, rows), UnityEngine.Random.Range(0, columns)].transform.position, Quaternion.identity);
            GameObject mazeGoal_Get = Instantiate(mazeGoal, nextCellStore.transform.position, Quaternion.identity);
            mazeGoal_Get.transform.SetParent(mazeParent.transform);

            ballSpawned = true;
        }
    }

    private MazeCell GetUnvisitedNeighbor(MazeCell currentCell)
    {
        var unvisitedNeighbors = FindAllUnvisitedNeighbors(currentCell);

        return unvisitedNeighbors.OrderBy(_ => UnityEngine.Random.Range(1, 10)).FirstOrDefault();
    }

    IEnumerable<MazeCell> FindAllUnvisitedNeighbors(MazeCell currentCell)
    {
        int x = (int)(currentCell.transform.localPosition.x * mazeParent.transform.lossyScale.x);
        int y = (int)(currentCell.transform.localPosition.y * mazeParent.transform.lossyScale.y);

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

        if (x - 1 >= 0)
        {
            MazeCell leftCell = maze[x - 1, y];

            if (!leftCell.visited)
            {
                yield return leftCell;
            }
        }

        if (y - 1 >= 0)
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
        if (currentCell.transform.localPosition.x < previousCell.transform.localPosition.x)
        {
            currentCell.SetWallState("right");
            previousCell.SetWallState("left");
            return;
        }
        if (currentCell.transform.localPosition.x > previousCell.transform.localPosition.x)
        {
            currentCell.SetWallState("left");
            previousCell.SetWallState("right");
            return;
        }
        if (currentCell.transform.localPosition.y < previousCell.transform.localPosition.y)
        {
            currentCell.SetWallState("top");
            previousCell.SetWallState("bottom");
            return;
        }
        if (currentCell.transform.localPosition.y > previousCell.transform.localPosition.y)
        {
            currentCell.SetWallState("bottom");
            previousCell.SetWallState("top");
            return;
        }
    }
}
