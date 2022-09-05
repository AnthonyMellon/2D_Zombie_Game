using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entities/Entity")]
public class Entity_SO : ScriptableObject
{
    public new string name;

    [Header("Health")]
    public float currentHealth;
    public float maxHealth;
    public floatSO currentHealthExt;
    public floatSO maxHealthExt;


    [Header("Movement")]
    public float moveSpeed;
    public Vector2 position;

    public List<Weapon_SO> weaponsInv;
    public Weapon_SO currentWeapon;

    public void Setup()
    {
        currentHealth = maxHealth;
        if(currentHealthExt) currentHealthExt.value = currentHealth;
        if(maxHealthExt) maxHealthExt.value = maxHealth;

    }

    public void UpdateExtVals()
    {
        if(currentHealthExt)
            if (currentHealthExt.value != currentHealth) currentHealthExt.value = currentHealth;
    }
}
