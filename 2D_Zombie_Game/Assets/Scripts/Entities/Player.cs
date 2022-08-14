using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    [Header("Input")]
    public Joystick joystickMovement;
    public Joystick joystickAim;

    [Header("HUD")]
    public TMP_Text health;
    public TMP_Text ammo;
    
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    private Weapon currentWeapon;
    private Rigidbody2D rb;


    private void Start()
    {
        base.Start();
        currentWeapon = transform.Find("weapon").GetComponent<Weapon>();
        rb = transform.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        base.Update();
        updateHUD();
        horizontalMovement = joystickMovement.Horizontal * moveSpeed;
        verticalMovement = joystickMovement.Vertical * moveSpeed;        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        //transform.Translate(new Vector3(horizontalMovement, verticalMovement, 0));
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, verticalMovement * moveSpeed);
    }

    private void updateHUD()
    {
        Debug.Log(currentHealth);
        health.text = $"Health: {currentHealth}/{maxHealth}";
        ammo.text = $"Ammo: {currentWeapon.ammoInMag}/{currentWeapon.magSize}";
    }
}
