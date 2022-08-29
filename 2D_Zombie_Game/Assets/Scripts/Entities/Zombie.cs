using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    [Header("Path Finding")]
    public GameObject target;
    public bool enableAI;
    public bool showPath;
    public int numStepsBeforePathUpdate;
    private int numStepsSinceLastPathUpdate;

    [SerializeField]
    private zombieManager manager;

    public Player_SO player;
    public floatSO playerMoney;

    [HideInInspector] public List<pathCell> path { get; private set; }

    private void Start()
    {
        base.onStart();
        manager = transform.parent.GetComponent<zombieManager>();        
        numStepsSinceLastPathUpdate = numStepsBeforePathUpdate;
        self = Instantiate(self);
    }

    private void Update()
    {
        base.onUpdate();
    }

    private void FixedUpdate()
    {
        if (enableAI)
        {
            Move();

            Vector2 shootDir = (player.position - self.position).normalized;
            weaponManager.shoot(shootDir.x, shootDir.y);
        }
                   
    }

    private void Move()
    {
        if(path != null && path.Count > 0)
        {            
            Vector2 targetPos = path[0].worldPos;
            transform.position = Vector2.MoveTowards(transform.position, targetPos, self.moveSpeed * Time.deltaTime);

            if ((Vector2)transform.position == targetPos)
            {
                path.RemoveAt(0);
                numStepsSinceLastPathUpdate++;
                if (numStepsSinceLastPathUpdate >= numStepsBeforePathUpdate) updatePath();
            }
        }
        else
        {
            updatePath();
        }

        if (showPath && path != null) drawPath();
    }

    private void faceTarget()
    {
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = new Vector2(0, 0);

        if(path != null && path[0] != null)
        {
            targetPosition = path[0].worldPos;
        }

        float distX = targetPosition.x - currentPosition.x;
        float distY = targetPosition.y - currentPosition.y;
        float theta = Mathf.Atan2(distY, distX) * (180 / Mathf.PI);

        transform.rotation = Quaternion.Euler(0, 0, theta);
    }

    private void updatePath()
    {        
        Vector2Int myGridPos = manager.pathFinder.grid.worldPosToGridPos(transform.position);
        Vector2Int targGridPos = manager.pathFinder.grid.worldPosToGridPos(target.transform.position);
        path = manager.pathFinder.FindPath(myGridPos.x, myGridPos.y, targGridPos.x, targGridPos.y);
        path.RemoveAt(0);
        numStepsSinceLastPathUpdate = 0;        
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

    public override void Damage(float damageValue)
    {
        base.Damage(damageValue);
        playerMoney.value += damageValue;
        Debug.Log($"player money is now at ${playerMoney.value}");
        StartCoroutine(DamageAnim());
    }

    private IEnumerator DamageAnim()
    {
        //Temp Damage animation for debug
        transform.GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(.1f);
        transform.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
/*        if(collision.transform.tag == "Player")
        {
            player.currentHealth -= self.currentWeapon.damage;
        }*/
    }
}
