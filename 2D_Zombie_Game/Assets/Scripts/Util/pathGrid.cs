using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathGrid
{
    public int numCellsX { get; private set; }
    public int numCellsY { get; private set; }
    private float cellSize;
    private Vector2 origin;

    public pathCell[,] cells { get; private set; }

    public pathGrid(int numCellsX, int numCellsY, float cellSize, Vector2 origin)
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
                float worldPosX = origin.x + (cellSize * (x - (numCellsX / 2)));
                float worldPosY = origin.y + (cellSize * (y - (numCellsY / 2)));

                cells[x, y] = new pathCell(x, y, worldPosX, worldPosY);
                cells[x, y].determineWalkability();
            }
        }
    }
}
