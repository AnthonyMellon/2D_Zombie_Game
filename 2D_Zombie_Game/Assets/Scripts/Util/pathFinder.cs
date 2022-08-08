using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pathFinder
{
    public pathGrid grid { get; private set; }
    public pathFinder()
    {
        grid = new pathGrid(43, 25, .5f, new Vector2(0, 0));
        grid.setup();
    }
}
