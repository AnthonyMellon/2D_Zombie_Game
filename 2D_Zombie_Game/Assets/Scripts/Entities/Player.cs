using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Player : Entity
{
    [Header("Input")]
    [SerializeField] private Joystick joystickMovement;
    [SerializeField] private Joystick joystickAim;
    [Range(0, 1)]
    [SerializeField] private float aimDeadZone;
    [SerializeField] private const KeyCode RELOAD_KEY = KeyCode.R;

    [Header("HUD")]
    public TMP_Text health;
    public TMP_Text ammo;

    [Header("Events")]
    public VoidEvent OnDeath;
    
    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        base.onStart();
        rb = transform.GetComponent<Rigidbody2D>();        
    }

    private void Update()
    {
        base.onUpdate();
        handleInputs();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void handleInputs()
    {
        moveInput();
        weaponInput();
        inventoryInput();
    }

    private void moveInput()
    {
        horizontalMovement = joystickMovement.Horizontal * self.moveSpeed;
        verticalMovement = joystickMovement.Vertical * self.moveSpeed;
    }

    private void weaponInput()
    {
        if (Mathf.Abs(joystickAim.Horizontal) > aimDeadZone || Mathf.Abs(joystickAim.Vertical) > aimDeadZone)
        {
            weaponManager.shoot(joystickAim.Horizontal, joystickAim.Vertical);
        }

        //Manual reload
        if (Input.GetKeyDown(RELOAD_KEY))
        {
            weaponManager.tryReload();
        }

    }

    private void inventoryInput()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            swapWeapon(self.weaponsInv[0]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swapWeapon(self.weaponsInv[1]);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            swapWeapon(self.weaponsInv[2]);
        }
    }

    private void Move()
    {
        rb.velocity = new Vector2(horizontalMovement * self.moveSpeed, verticalMovement * self.moveSpeed);
    }

    protected override void Die()
    {
        OnDeath.Raise();
    }
}
