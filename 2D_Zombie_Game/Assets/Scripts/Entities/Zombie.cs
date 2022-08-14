using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    [Header("Path Finding")]
    public GameObject target;
    public bool enableAI;
    public bool showPath;
    public int pathFindDelay;
    private int pathWait;

    private zombieManager manager;

    [HideInInspector] public List<pathCell> path { get; private set; }

    private void Start()
    {
        base.Start();
        manager = transform.parent.GetComponent<zombieManager>();
        pathWait = pathFindDelay;
    }

    private void FixedUpdate()
    {
        if (enableAI) Move();       
    }

    private void Move()
    {
        getPath();

        //Face in the direction of the target 
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.transform.position;

        float distX = targetPosition.x - currentPosition.x;
        float distY = targetPosition.y - currentPosition.y;
        float theta = Mathf.Atan2(distY, distX) * (180/Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, theta);
       
        //Move towards the target
        //transform.Translate(moveSpeed, 0, 0);
    }

    private void getPath()
    {
        pathWait ++;

        if(pathWait >= pathFindDelay)
        {
            Vector2Int myGridPos = manager.pathFinder.grid.worldPosToGridPos(transform.position);
            Vector2Int targGridPos = manager.pathFinder.grid.worldPosToGridPos(target.transform.position);
            path = manager.pathFinder.FindPath(myGridPos.x, myGridPos.y, targGridPos.x, targGridPos.y);            
            pathWait = 0;            
        }
        if (showPath) drawPath();

    }

    private void drawPath()
    {
        for(int i = 1; i < path.Count; i++)
        {
            Debug.DrawLine(path[i - 1].worldPos, path[i].worldPos, Color.red);
        }
    }

    protected override void Die()
    {
        base.Die();
        GameObject.Destroy(gameObject);
    }

    public void Damage(float damageValue)
    {
        base.Damage(damageValue);
        StartCoroutine(DamageAnim());

    }

    private IEnumerator DamageAnim()
    {
        //Temp Damage animation for debug
        transform.Find("ZombieSprite").GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        transform.Find("ZombieSprite").GetComponent<SpriteRenderer>().color = Color.white;
    }
}
