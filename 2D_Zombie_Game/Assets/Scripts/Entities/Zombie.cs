using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Entity
{
    [Header("Path Finding")]
    public GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
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
}
