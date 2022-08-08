using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCell
{
    private Vector2Int absPos;
    public Vector2 wrldPos { get; private set; }

    public bool walkable { get; private set; }

    public Color col { get; private set; }

    public pathCell(int absPosX, int absPosY, float wrldPosX, float wrldPosY)
    {
        absPos = new Vector2Int(absPosX, absPosY);
        wrldPos = new Vector2(wrldPosX, wrldPosY);       
    }

    public void determineWalkability()
    {
        RaycastHit2D hit = Physics2D.Raycast(wrldPos, Vector2.up, .1f);        
        walkable = true;
        col = Color.green;
        if (hit && hit.transform.tag == "wall")
        {
            walkable = false;
            col = Color.red;
        }
    } 
}

    