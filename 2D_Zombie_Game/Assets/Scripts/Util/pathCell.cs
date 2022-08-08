using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathCell
{
    private Vector2Int absPos;
    private Vector2 wrldPos;

    public bool walkable { get; private set; }

    public pathCell(int absPosX, int absPosY, float wrldPosX, float wrldPosY)
    {
        absPos = new Vector2Int(absPosX, absPosY);
        wrldPos = new Vector2(wrldPosX, wrldPosY);       
    }

    public void determineWalkability()
    {
        RaycastHit2D hit = Physics2D.Raycast(wrldPos, Vector2.up, .1f);
        walkable = true;
        if (hit && hit.transform.tag == "wall")
        {
            Debug.Log(hit.transform.name);
            walkable = false;
        }
    } 
}

    