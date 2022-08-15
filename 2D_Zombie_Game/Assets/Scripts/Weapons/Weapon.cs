using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon_SO self;

    [Header("Input")]
    [SerializeField] private Joystick weaponJoystick;
    [Range(0, 1)]
    [SerializeField] private float joystickDeadZone;
    [SerializeField]private const KeyCode RELOAD_KEY = KeyCode.R;
    

    // Start is called before the first frame update
    void Start()
    {
        self.Setup();
    }

    // Update is called once per frame
    void Update()
    {
        //Shoot when the joystick is moved outside the deadzone, and if the player as ammo to shoot
        Debug.DrawRay(transform.parent.position, new Vector2(weaponJoystick.Horizontal, weaponJoystick.Vertical) * self.range, Color.yellow);
        if (Mathf.Abs(weaponJoystick.Horizontal) > joystickDeadZone || Mathf.Abs(weaponJoystick.Vertical) > joystickDeadZone)
        {
            if (self.ammoInMag > 0 && self.roundLoaded) shoot(weaponJoystick.Vertical, weaponJoystick.Horizontal);
        } 
        
        //Manual reload
        if(Input.GetKeyDown(RELOAD_KEY))
        {
            if(!self.reloading && self.ammoInMag < self.magSize)
            {
                StartCoroutine(reloadMag());
            }
        }
    }    

    private void shoot(float vert, float horiz)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.parent.position, new Vector2(horiz, vert), self.range);        

        if(hit)
        {
            if(hit.transform.parent != null)
            {
                if (hit.transform.parent.transform.tag == "Zombie")
                {
                    Zombie hitScript = hit.transform.parent.transform.GetComponent<Zombie>();
                    hitScript.Damage(self.damage);
                }
            }
        }

        StartCoroutine(cycleRound());

        //Automatic reload
        if (self.ammoInMag == 0)
        {
            StartCoroutine(reloadMag());
        }
    }

    //Cycles the round in the weapon
    private IEnumerator cycleRound()
    {
        self.ammoInMag -= 1;
        self.roundLoaded = false;
        yield return new WaitForSeconds(1 / self.fireRate);
        self.roundLoaded = true;
    }

    //Used after the entier mag is spent
    private IEnumerator reloadMag()
    {        
        if(!self.reloading)
        {
            self.reloading = true;
            yield return new WaitForSeconds(self.reloadSpeed);
            self.ammoInMag = self.magSize;
            self.reloading = false;
        }        
       
    }
}
