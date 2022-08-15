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
        horizontalMovement = joystickMovement.Horizontal * self.moveSpeed;
        verticalMovement = joystickMovement.Vertical * self.moveSpeed;        
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalMovement * self.moveSpeed, verticalMovement * self.moveSpeed);
    }

    private void updateHUD()
    {
        //health.text = $"Health: {self.currentHealth}/{self.maxHealth}";
        ammo.text = $"Ammo: {currentWeapon.ammoInMag}/{currentWeapon.magSize}";
    }
}
