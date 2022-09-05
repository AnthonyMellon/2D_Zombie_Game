using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{    
    public Entity_SO self;

    protected Weapon weaponManager;

    protected void onStart()
    {
        self.Setup();
        weaponManager = transform.Find("weaponManager").GetComponent<Weapon>();
        swapWeapon(self.weaponsInv[0]);
    }

    protected void onUpdate()
    {
        if (self.currentHealth <= 0) Die();
        self.position = transform.position;
    }

    public virtual void Damage(float damageValue)
    {
        self.currentHealth -= damageValue;
    }

    protected virtual void Die()
    {

    }

    public void swapWeapon(Weapon_SO newWeapon)
    {
        self.currentWeapon = newWeapon;
        self.currentWeapon.Setup();
        weaponManager.currentWeapon = self.currentWeapon;
    }
}
