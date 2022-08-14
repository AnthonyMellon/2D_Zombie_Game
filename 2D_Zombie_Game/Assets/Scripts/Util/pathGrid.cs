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
                Vector2 worldPos = gridPosToWorldPos(new Vector2Int(x, y));

                cells[x, y] = new pathCell(x, y, worldPos.x, worldPos.y);
                cells[x, y].determineWalkability();
            }
        }
    }

    public Vector2Int worldPosToGridPos(Vector2 worldPos)
    {
        int gridX = Mathf.RoundToInt(((worldPos.x - origin.x) / cellSize) + (numCellsX / 2));
        int gridY = Mathf.RoundToInt(((worldPos.y - origin.y) / cellSize) + (numCellsY / 2));

        return new Vector2Int(gridX, gridY);
    }

    public Vector2 gridPosToWorldPos(Vector2Int gridPos)
    {
        float worldX = origin.x + (cellSize * (gridPos.x - (numCellsX / 2)));
        float worldY = origin.y + (cellSize * (gridPos.y - (numCellsY / 2)));

        return new Vector2(worldX, worldY);
    }
}
