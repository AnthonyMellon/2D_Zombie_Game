using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinder
{
    private const int MOVE_STRAIGHT_COST = 10;
    private const int MOVE_DIAGONAL_COST = 14;

    public pathGrid grid { get;  private set; }
    private List<pathCell> openList;
    private List<pathCell> closedList;

    public pathFinder()
    {
        grid = new pathGrid(170, 98, .5f, new Vector2(0, 0));
        grid.setup();
    }

    public List<pathCell> FindPath(int startX, int startY, int endX, int endY)
    {
        pathCell startNode = grid.cells[startX, startY];
        pathCell endNode = grid.cells[endX, endY];

        openList = new List<pathCell> { startNode };
        closedList = new List<pathCell>();

        for(int x = 0; x < grid.numCellsX; x++)
        {
            for(int y = 0; y < grid.numCellsY; y++)
            {
                pathCell currentNode = grid.cells[x, y];
                currentNode.g = int.MaxValue;
                currentNode.calcFCost();
                currentNode.parent = null;
            }
        }

        startNode.g = 0;
        startNode.h = calcDistance(startNode.gridPos, endNode.gridPos);
        startNode.calcFCost();

        while(openList.Count > 0)
        {
            pathCell currentNode = getLowestFCostNode();
            if (currentNode.gridPos == endNode.gridPos)
            {
                //Reached final node
                return (calculatePath(endNode));
            }

            openList.Remove(currentNode);
            closedList.Add(currentNode);

            foreach(pathCell neighbourNode in getNeighboursList(currentNode))
            {
                if (closedList.Contains(neighbourNode)) continue;
                if (!neighbourNode.walkable) continue;

                int tentativeGCost = currentNode.g + calcDistance(currentNode.gridPos, neighbourNode.gridPos);
                if(tentativeGCost < neighbourNode.g)
                {
                    neighbourNode.parent = currentNode;
                    neighbourNode.g = tentativeGCost;
                    neighbourNode.h = calcDistance(neighbourNode.gridPos, endNode.gridPos);
                    neighbourNode.calcFCost();

                    if(!openList.Contains(neighbourNode))
                    {
                        openList.Add(neighbourNode);
                    }
                }
            }
        }

        //Out of node on the open list
        return null;
    }

    private List<pathCell> getNeighboursList(pathCell currentCell)
    {
        List<pathCell> neighbourList = new List<pathCell>();
        Vector2Int currentPos = currentCell.gridPos;

        if(currentPos.x - 1 >= 0)
        {
            //Left
            neighbourList.Add(grid.cells[currentPos.x - 1, currentPos.y]);
            //Left Down
            if (currentPos.y - 1 >= 0) neighbourList.Add(grid.cells[currentPos.x - 1, currentPos.y - 1]);
            //Left Up
            if (currentPos.y + 1 < grid.numCellsY) neighbourList.Add(grid.cells[currentPos.x - 1, currentPos.y + 1]);
        }

        if(currentPos.x + 1 < grid.numCellsX)
        {
            //Right
            neighbourList.Add(grid.cells[currentPos.x + 1, currentPos.y]);
            //Right Down
            if (currentPos.y - 1 >= 0) neighbourList.Add(grid.cells[currentPos.x + 1, currentPos.y - 1]);
            //Right Up
            if (currentPos.y + 1 < grid.numCellsY) neighbourList.Add(grid.cells[currentPos.x + 1, currentPos.y + 1]);
        }

        //Down
        if (currentPos.y - 1 >= 0) neighbourList.Add(grid.cells[currentPos.x, currentPos.y - 1]);
        //Up
        if (currentPos.y + 1 < grid.numCellsY) neighbourList.Add(grid.cells[currentPos.x, currentPos.y + 1]);

        return neighbourList;
    }

    private List<pathCell> calculatePath(pathCell endNode)
    {
        List<pathCell> path = new List<pathCell>();
        path.Add(endNode);
        pathCell currentNode = endNode;
        while(currentNode.parent != null)
        {
            path.Add(currentNode.parent);
            currentNode = currentNode.parent;
        }

        path.Reverse();
        return path;
    }

    private pathCell getLowestFCostNode()
    {
        pathCell lowestFCostNode = openList[0];
        for(int x = 0; x < openList.Count; x++)
        {
            if (openList[x].f < lowestFCostNode.f)
            {
                lowestFCostNode = openList[x];
            }
        }
        return lowestFCostNode;
    }

    private int calcDistance(Vector2Int pos1, Vector2Int pos2)
    {
        int xDistance = Mathf.Abs(pos1.x - pos2.x);
        int yDistance = Mathf.Abs(pos1.y - pos2.y);
        int remaining = Mathf.Abs(xDistance - yDistance);
        return MOVE_DIAGONAL_COST * Mathf.Min(xDistance, yDistance) + MOVE_STRAIGHT_COST * remaining;
    }
}