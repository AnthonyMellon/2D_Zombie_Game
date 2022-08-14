using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCell
{
    public Vector2Int gridPos { get; private set; }
    public Vector2 worldPos { get; private set; }
    public bool walkable { get; private set; }
    public Color col { get; private set; }
    [HideInInspector] public int f = 0;
    [HideInInspector] public int g = 0;
    [HideInInspector] public int h = 0;
    public pathCell parent;

    public pathCell(int absPosX, int absPosY, float wrldPosX, float wrldPosY)
    {
        gridPos = new Vector2Int(absPosX, absPosY);
        worldPos = new Vector2(wrldPosX, wrldPosY);       
    }

    public void determineWalkability()
    {
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.up, .1f);        
        walkable = true;
        col = Color.green;
        if (hit && hit.transform.tag == "wall")
        {
            walkable = false;
            col = Color.red;
        }
    } 
    public void calcFCost()
    {
        f = g + h;
    }
}

    