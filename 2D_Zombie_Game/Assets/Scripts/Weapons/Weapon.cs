using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Joystick weaponJoystick;
    [Range(0, 1)]
    [SerializeField] private float joystickDeadZone;
    private const KeyCode RELOAD_KEY = KeyCode.R;

    [Header("Weapon Stats")]    
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [Tooltip("Number of bullets fired per second")]
    [SerializeField] private float fireRate;
    [SerializeField] public float magSize;
    [Tooltip("How many seconds it takes to reload")]
    [SerializeField] private float reloadSpeed;    

    [Header("Debug")]    
    [SerializeField] private bool roundLoaded = true;
    [SerializeField] private bool reloading = false;
    [SerializeField] public float ammoInMag;

    // Start is called before the first frame update
    void Start()
    {
        ammoInMag = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot when the joystick is moved outside the deadzone, and if the player as ammo to shoot
        Debug.DrawRay(transform.parent.position, new Vector2(weaponJoystick.Horizontal, weaponJoystick.Vertical) * range, Color.yellow);
        if (Mathf.Abs(weaponJoystick.Horizontal) > joystickDeadZone || Mathf.Abs(weaponJoystick.Vertical) > joystickDeadZone)
        {
            if (ammoInMag > 0 && roundLoaded) shoot(weaponJoystick.Vertical, weaponJoystick.Horizontal);
        } 
        
        //Manual reload
        if(Input.GetKeyDown(RELOAD_KEY))
        {
            if(!reloading && ammoInMag < magSize)
            {
                StartCoroutine(reloadMag());
            }
        }
    }    

    private void shoot(float vert, float horiz)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.parent.position, new Vector2(horiz, vert), range);        

        if(hit)
        {
            if(hit.transform.parent != null)
            {
                if (hit.transform.parent.transform.tag == "Zombie")
                {
                    Zombie hitScript = hit.transform.parent.transform.GetComponent<Zombie>();
                    hitScript.Damage(damage);
                }
            }
        }

        StartCoroutine(cycleRound());

        //Automatic reload
        if (ammoInMag == 0)
        {
            StartCoroutine(reloadMag());
        }
    }

    //Cycles the round in the weapon
    private IEnumerator cycleRound()
    {
        ammoInMag -= 1;
        roundLoaded = false;
        yield return new WaitForSeconds(1 / fireRate);
        roundLoaded = true;
    }

    //Used after the entier mag is spent
    private IEnumerator reloadMag()
    {        
        if(!reloading)
        {
            reloading = true;
            yield return new WaitForSeconds(reloadSpeed);
            ammoInMag = magSize;
            reloading = false;
        }        
       
    }
}
