using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    [Header("Path Finding")]
    public GameObject target;
    public bool enableAI;

    private void FixedUpdate()
    {
        if (enableAI) Move();       
    }

    private void Move()
    {
        //Face in the direction of the target 
        Vector3 currentPosition = transform.position;
        Vector3 targetPosition = target.transform.position;

        Debug.DrawLine(currentPosition, targetPosition, Color.red);

        float distX = targetPosition.x - currentPosition.x;
        float distY = targetPosition.y - currentPosition.y;
        float theta = Mathf.Atan2(distY, distX) * (180/Mathf.PI);
        transform.rotation = Quaternion.Euler(0, 0, theta);
       
        //Move towards the target
        transform.Translate(moveSpeed, 0, 0);
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
