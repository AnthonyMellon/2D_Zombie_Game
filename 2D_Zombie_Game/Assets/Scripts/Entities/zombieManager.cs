using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zombieManager : MonoBehaviour
{
    [Header("Debug")]
    public bool showGrid;
    public bool showPaths;
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
                    Gizmos.DrawSphere(pathFinder.grid.cells[x, y].worldPos, .1f);
                    Gizmos.color = Color.grey;
                    Gizmos.DrawWireCube(pathFinder.grid.cells[x, y].worldPos, new Vector3(.5f, .5f, .1f));
                }
            }
        }

        if(pathFinder != null)
        {
            for(int i = 0; i < transform.childCount; i++)
            {
                Zombie zomb = transform.GetChild(i).GetComponent<Zombie>();
                if(zomb.showPath)
                {
                    Gizmos.color = Color.yellow;
                    for(int j = 1; j < zomb.path.Count; j++)
                    {
                        Gizmos.DrawLine(zomb.path[j - 1].worldPos, zomb.path[j].worldPos);
                    }
                }
                
            }
        }
    }
}
