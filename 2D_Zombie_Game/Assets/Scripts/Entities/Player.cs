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

    [Header("Stats")]
    [SerializeField] private floatSO startMoney;
    [SerializeField] private floatSO money;

    [Header("Animation")]
    public Animator anim;

    [Header("Sounds")]
    public AudioSource walkSound;

    private float horizontalMovement = 0f;
    private float verticalMovement = 0f;

    private Rigidbody2D rb;

    private void Start()
    {
        base.onStart();
        rb = transform.GetComponent<Rigidbody2D>();
        money.value = startMoney.value;
    }

    private void Update()
    {
        base.onUpdate();
        handleInputs();
    }

    private void FixedUpdate()
    {
        Move();
        self.UpdateExtVals();
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

        if (Mathf.Abs(horizontalMovement) > 0 || Mathf.Abs(verticalMovement) > 0)
        {
            walkSound.mute = false;
            anim.SetBool("Walking", true);
        }
        else
        {
            walkSound.mute = true;
            anim.SetBool("Walking", false);
        }

/*        if (horizontalMovement > 0) transform.localScale = new Vector3(8, 8, 1);
        else if (horizontalMovement < 0) transform.localScale = new Vector3(-8, 8, 1);*/
    }

    private void weaponInput()
    {
        if (Mathf.Abs(joystickAim.Horizontal) > aimDeadZone || Mathf.Abs(joystickAim.Vertical) > aimDeadZone)
        {
            if(weaponManager.shoot(joystickAim.Horizontal, joystickAim.Vertical)) anim.SetTrigger("Attack");
            if (joystickAim.Horizontal > 0) transform.localScale = new Vector3(8, 8, 1);
            else transform.localScale = new Vector3(-8, 8, 1);

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
            swapWeapon(self.weaponsInv[0], true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            swapWeapon(self.weaponsInv[1], true);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            swapWeapon(self.weaponsInv[2], true);
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
