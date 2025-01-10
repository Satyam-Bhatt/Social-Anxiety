using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class MazeGenerator : MonoBehaviour
{
    private static MazeGenerator instance;
    public static MazeGenerator Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MazeGenerator>();
            }
            return instance;
        }
    }

    [SerializeField]
    private GameObject mazeCell, mazeBall, mazeGoal;

    [SerializeField] private Color winColor;

    private GameObject mazeParent;

    public GameObject[] mazeLevels;

    [SerializeField]
    private MovementSystem movementSystem;
    private CoffeeGame coffeeGame;

    [SerializeField]
    private int rows, columns;

    private MazeCell[,] maze;

    private bool done, ballSpawned = false;
    private int level = 1;

    private bool playOnce = false;
    [HideInInspector] public bool inRange = true;

    private PlayerControls playerControls;

    [SerializeField] private Eye_Player eye_Player;

    [Header("Canvas")]
    [Space(10)]
    public GameObject arrow;
    [SerializeField] private float xDifference = 0f;
    public GameObject t1, t2;

    private void Awake()
    {
        coffeeGame = movementSystem.GetComponent<CoffeeGame>();
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.CoffeGame.BallMovement.Enable();
        playerControls.CoffeGame.BallMovement.performed += BallButtonsPressed;
    }

    private void OnDisable()
    {
        playerControls.CoffeGame.BallMovement.Disable();
        playerControls.CoffeGame.BallMovement.performed -= BallButtonsPressed;
    }

    private void Start()
    {
        foreach (GameObject g in mazeLevels)
        {
            g.SetActive(false);
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        //LoadMaze();
    }

    public void LoadNextMaze()
    {
        StopAllCoroutines();
        StartCoroutine(LoadMaze_Win());
    }

    IEnumerator LoadMaze_Win()
    {
        GameObject[] cell = GameObject.FindGameObjectsWithTag("CellBottom");
        foreach (GameObject m in cell)
        {
            m.GetComponent<SpriteRenderer>().color = winColor;
        }

        GetComponent<AudioSource>().Play();

        if (level == 1)
        {
            level++;
        }
        else if (level > 1 && GameManager.Instance.audioSrc.isPlaying == false)
        {
            level++;
            coffeeGame.EnableSprites();
            playOnce = false;
        }

        if (level == 3)
        {
            GetComponent<PlayerFollow>().distanceX = -4.34f;
            GetComponent<PlayerFollow>().distanceY = 0;
            eye_Player.EnableForLevel3();
        }

        yield return new WaitForSeconds(0.5f);

        if (mazeParent != null)
        {
            for (int i = 0; i < mazeParent.transform.childCount; i++)
            {
                Destroy(mazeParent.transform.GetChild(i).gameObject);
            }
        }

        if (level > mazeLevels.Length)
        {
            level = 4;
        }

        if (level - 1 < mazeLevels.Length && mazeLevels[level - 1] != null)
        {
            mazeLevels[level - 1].SetActive(true);
            rows = mazeLevels[level - 1].GetComponent<MazeStats>().rows;
            columns = mazeLevels[level - 1].GetComponent<MazeStats>().columns;
            CreateMaze(mazeLevels[level - 1].transform);
            done = false; ballSpawned = false;
        }
    }

    public void LoadMaze()
    {
        StopAllCoroutines();

        if (mazeParent != null)
        {
            for (int i = 0; i < mazeParent.transform.childCount; i++)
            {
                if (!mazeParent.transform.GetChild(i).CompareTag("Instruction"))
                    Destroy(mazeParent.transform.GetChild(i).gameObject);
            }
        }

        foreach (GameObject g in mazeLevels)
        {
            g.SetActive(false);
        }

        if (level - 1 < mazeLevels.Length && mazeLevels[level - 1] != null)
        {
            mazeLevels[level - 1].SetActive(true);
            rows = mazeLevels[level - 1].GetComponent<MazeStats>().rows;
            columns = mazeLevels[level - 1].GetComponent<MazeStats>().columns;
            CreateMaze(mazeLevels[level - 1].transform);
            done = false; ballSpawned = false;
        }
    }

    public void ActiveDeactivateMaze(bool state)
    {
        if (level - 1 < mazeLevels.Length && mazeLevels[level - 1] != null)
        {
            mazeLevels[level - 1].SetActive(state);
        }
    }

    public void ActiveDeactivateChild(bool state)
    {
        StopAllCoroutines();
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(state);
        }
    }
    
    //Plays Audio
    private void BallButtonsPressed(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (mazeLevels[level - 1].activeSelf)
            {
                //if (playOnce == false && coffeeGame.keyIndex == 1)
                //{
                //    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(15);
                //    playOnce = true;
                //}

                if (coffeeGame.keyIndex == 2 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(16);
                    playOnce = true;
                }
                else if (coffeeGame.keyIndex == 3 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(17);
                    playOnce = true;
                }
                else if (coffeeGame.keyIndex == 4 && !playOnce)
                {
                    GameManager.Instance.GetComponent<RandomThoughts>().ClipPlay_Immediate(18);
                    playOnce = true;
                }
            }
        }
    }

    public void PointArrow()
    {
        inRange = false;

        GameObject obj = null;
        foreach (GameObject g in coffeeGame.coffeeActivator)
        {
            if (g.activeSelf)
            {
                obj = g;
                break;
            }
        }

        arrow.SetActive(true);
        if (obj != null)
        {
            arrow.transform.position = new Vector3(obj.transform.position.x - xDifference, obj.transform.position.y, obj.transform.position.z);
        }

        t1.SetActive(true);
        t2.SetActive(true);
        t1.transform.parent.GetChild(0).gameObject.SetActive(true);

        t1.GetComponent<TMP_Text>().text = "Walk Over To -";
        if (coffeeGame.coffeeActivator[0].activeSelf)
        {
            t2.GetComponent<TMP_Text>().text = "Coffee Grinder";
        }
        else if (coffeeGame.coffeeActivator[1].activeSelf)
        {
            t2.GetComponent<TMP_Text>().text = "Stove";
        }
        else if (coffeeGame.coffeeActivator[2].activeSelf)
        {
            t2.GetComponent<TMP_Text>().text = "Brewer";
        }
        else if (coffeeGame.coffeeActivator[3].activeSelf)
        {
            t2.GetComponent<TMP_Text>().text = "Coffee Cup";
        }

    }

    private void MazeGeneratorComplete()
    {
        //mazeParent.transform.localScale = new Vector3(0.97f, 0.97f, 0.97f);
    }

    public void CreateMaze(Transform mazeParent_)
    {
        mazeParent = mazeParent_.gameObject;

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
            /*            float scaleX = mazeBall_Get.transform.localScale.x;
                        float scaleY = mazeBall_Get.transform.localScale.y;
                        mazeBall_Get.transform.localScale = new Vector3(scaleX - 1, scaleY - 1, 1f);*/

            //Instantiate Goal
            int row_Index = UnityEngine.Random.Range(0, rows);
            int column_Index = UnityEngine.Random.Range(0, columns);
            if (row_Index == 0 && column_Index == 0)
            {
                MazeStats mazeStats = mazeParent.GetComponent<MazeStats>();
                row_Index = mazeStats.goalPlacementrow;
                column_Index = mazeStats.goalPlacementcolumn;
            }

            GameObject mazeGoal_Get = null;
            if(maze[row_Index, column_Index] != null)
                mazeGoal_Get = Instantiate(mazeGoal, maze[row_Index, column_Index].transform.position, Quaternion.identity);
            //MazeStats mazeStats = mazeParent.GetComponent<MazeStats>();
            //GameObject mazeGoal_Get = Instantiate(mazeGoal, maze[mazeStats.goalPlacementrow, mazeStats.goalPlacementcolumn].transform.position, Quaternion.identity);
            if(mazeGoal_Get) mazeGoal_Get.transform.SetParent(mazeParent.transform);

            MazeGeneratorComplete();

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
        if (currentCell == null)
        {
            Debug.Log("Null Cell");
            yield return null;
/*            StopAllCoroutines();
            LoadMaze();
            yield return null;*/
        }

        else
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
