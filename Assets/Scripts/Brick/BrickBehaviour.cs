using System;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class BrickBehaviour : MonoBehaviour
{
    public List<Material> materials;
    public GameManager gameManager;
    public Vector2 position;

    //0:bottom, 1:right, 2:top, 3:left
    private GameObject[] neighbours = new GameObject[4];
    private int color;
    private bool destroyed = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        this.color = UnityEngine.Random.Range(0, materials.Count-1);
        GetComponent<SpriteRenderer>().material = materials[color];
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnMouseDown()
    {
        gameManager.toDelete = 0;
        KillNeighbourgs(this.color);
    }

    public void KillNeighbourgs(int color){
        SetNeighbours();
        destroyed = true;
        for (int indice = 0; indice < 4; indice++)
        {
            killNeighbourg(indice, color);
        }

        if (destroyed)
        {
            if (gameManager.toDelete>0)
            {
                gameManager.getGrid()[(int)position.x, (int)position.y] = null;
                Destroy(gameObject);
            }
            else
            {
                destroyed = false;
            }
        }
    }

    private void killNeighbourg(int indice, int color)
    {
        if (neighbours[indice])
        {
            BrickBehaviour bb = neighbours[indice].GetComponent<BrickBehaviour>();
            if (bb.color == color && !bb.destroyed)
            {
                gameManager.toDelete++;
                bb.KillNeighbourgs(bb.color);
            }
        }
    }

    public void SetNeighbours()
    {

        if (position.y > 0)
            neighbours[0] = gameManager.getGrid()[(int)position.x, (int)position.y - 1];

        if (position.x < gameManager.getGrid().GetLength(0)-1)
            neighbours[1] = gameManager.getGrid()[(int)position.x + 1, (int)position.y];

        if (position.y < gameManager.getGrid().GetLength(1)-1)
            neighbours[2] = gameManager.getGrid()[(int)position.x, (int)position.y + 1];

        if (position.x > 0)
            neighbours[3] = gameManager.getGrid()[(int)position.x - 1, (int)position.y];
    }
}
