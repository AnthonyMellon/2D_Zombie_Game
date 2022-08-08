using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathGrid
{
    private int numCellsX;
    private int numCellsY;
    private float cellSize;
    private Vector2 origin;

    private pathCell[,] cells;

    public pathGrid(int numCellsX, int numCellsY, int cellSize, Vector2 origin)
    {
        this.numCellsX = numCellsX;
        this.numCellsY = numCellsY;
        this.cellSize = cellSize;
        this.origin = origin;
    }

    public void setup()
    {
        cells = new pathCell[numCellsX, numCellsY];

        for(int x = 0; x < numCellsX; x++)
        {
            for(int y = 0; y < numCellsY; y++)
            {
                float worldPosX = origin.x + (cellSize * x);
                float worldPosY = origin.y + (cellSize * y);

                cells[x, y] = new pathCell(x, y, worldPosX, worldPosY);
                cells[x, y].determineWalkability();
            }
        }
    }
}
