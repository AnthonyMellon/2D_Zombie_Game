using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{    
    public Entity_SO self;

    public Weapon weaponManager;

    [Header("Sounds")]
    public AudioSource swapSound;

    protected void onStart()
    {
        self.Setup();
        swapWeapon(self.weaponsInv[0], false);
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

    public void swapWeapon(Weapon_SO newWeapon, bool playSound)
    {
        if (playSound) swapSound.Play();
        self.currentWeapon = newWeapon;
        self.currentWeapon.Setup();
        weaponManager.currentWeapon = self.currentWeapon;
    }
}
