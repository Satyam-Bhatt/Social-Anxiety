using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MazeCell : MonoBehaviour
{
    [SerializeField]
    private GameObject topWall, bottomWall, leftWall, rightWall, cellCap;

    public bool visited = false;

    //public Vector2 showLocation;

    //private void Update()
    //{
    //    showLocation = cellCap.transform.position;
    //}

    public void Visit()
    { 
        visited = true;
        cellCap.SetActive(false);
    }

    public void SetWallState(string wallName)
    {
        switch (wallName)
        {
            case "top":
                topWall.SetActive(false);
                break;
            case "bottom":
                bottomWall.SetActive(false);
                break;
            case "left":
                leftWall.SetActive(false);
                break;
            case "right":
                rightWall.SetActive(false);
                break;
        }
    }
}
