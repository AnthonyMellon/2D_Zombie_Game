using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieManager : MonoBehaviour
{
    [Header("Debug")]
    public bool showGrid;    
    public pathFinder pathFinder { get; private set; }
    // Start is called before the first frame update
    void Start()
    {
        pathFinder = new pathFinder();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDrawGizmos()
    {
        if(pathFinder != null && showGrid)
        {
            for (int x = 0; x < pathFinder.grid.cells.GetLength(0); x++)
            {
                for (int y = 0; y < pathFinder.grid.cells.GetLength(1); y++)
                {
                    Gizmos.color = pathFinder.grid.cells[x, y].col;
                    Gizmos.DrawSphere(pathFinder.grid.cells[x, y].wrldPos, .1f);
                    Gizmos.color = Color.grey;
                    Gizmos.DrawWireCube(pathFinder.grid.cells[x, y].wrldPos, new Vector3(.5f, .5f, .1f));
                }
            }
        }
    }
}
