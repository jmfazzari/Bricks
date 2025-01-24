using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject bricksBluePrint;
    public float stepBrick=2.7f;
    public Vector2 gridSize;

    private List<GameObject> bricks;
    private GameObject[,] grid = new GameObject[20,30];
    private Vector2 offset = new Vector2();

    public int toDelete = 0;

    public GameObject[,] getGrid()
    {
        return grid;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.offset.x = (this.gridSize.x - 1) * stepBrick / 2;
        this.offset.y = (this.gridSize.y - 1) * stepBrick / 2;

        this.bricks = new List<GameObject>();

        for(int x= 0; x < this.gridSize.x; x++)
        {
            for(int y= 0; y < gridSize.y; y++)
            {
                GameObject go = GameObject.Instantiate(bricksBluePrint, new Vector3(x * stepBrick - offset.x, y * stepBrick - offset.y, 0), Quaternion.identity);
                go.name = "go_"+x+"_"+y;
                BrickBehaviour bb = go.GetComponent<BrickBehaviour>();
                bb.gameManager = this;
                bb.position = new Vector2(x,y);
                this.grid[x, y] = go;
            }
        }
        for (int x = 0; x < this.gridSize.x; x++)
        {
            for (int y = 0; y < gridSize.y; y++)
            {
                this.grid[x, y].GetComponent<BrickBehaviour>().SetNeighbours();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
