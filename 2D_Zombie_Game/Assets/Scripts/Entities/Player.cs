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

    public Rigidbody2D rb;

    private void Start()
    {
        currentHealth = maxHealth;
        rb = transform.Find("PlayerSprite").GetComponent<Rigidbody2D>();
        pathGrid grid = new pathGrid(10, 18, 1, new Vector2(0, 0));
        grid.setup();
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
        horizontalMovement = joystickMovement.Horizontal * moveSpeed;
        verticalMovement = joystickMovement.Vertical * moveSpeed;        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
    }
}
