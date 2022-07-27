using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [Header("Input")]
    [SerializeField] private Joystick weaponJoystick;
    [Range(0, 1)]
    [SerializeField] private float joystickDeadZone;

    [Header("Weapon Stats")]    
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [Tooltip("Number of bullets fired per second")]
    [SerializeField] private float fireRate;
    [SerializeField] private float magSize;
    [Tooltip("How many seconds it takes to reload")]
    [SerializeField] private float reloadSpeed;

    [Header("Debug")]
    [SerializeField] private float ammoInMag;
    [SerializeField] private bool roundLoaded = true;

    // Start is called before the first frame update
    void Start()
    {
        ammoInMag = magSize;
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(weaponJoystick.Horizontal) > joystickDeadZone || Mathf.Abs(weaponJoystick.Vertical) > joystickDeadZone)
        {
            if (ammoInMag > 0 && roundLoaded) shoot();
        }
    }    

    private void shoot()
    {
        ammoInMag -= 1;
        roundLoaded = false;

        StartCoroutine(reloadRound());

        if (ammoInMag == 0)
        {
            StartCoroutine(reloadMag());
        }
    }

    //Used after every bullet is shot
    private IEnumerator reloadRound()
    {
        yield return new WaitForSeconds(1 / fireRate);
        roundLoaded = true;
    }

    //Used after the entier mag is spent
    private IEnumerator reloadMag()
    {
        yield return new WaitForSeconds(reloadSpeed);
        ammoInMag = magSize;
    }
}
