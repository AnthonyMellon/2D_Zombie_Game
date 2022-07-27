using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    [Header("Joysticks")]
    public Joystick joystickMovement;
    public Joystick joystickAim;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = joystickMovement.Horizontal * moveSpeed;
        verticalMovement = joystickMovement.Vertical * moveSpeed;        
    }

    private void FixedUpdate()
    {
        Move();
    }

    public override void Move()
    {
        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
    }
}
