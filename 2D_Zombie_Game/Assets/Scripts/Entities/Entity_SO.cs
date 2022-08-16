using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[CreateAssetMenu(fileName = "Entity", menuName = "ScriptableObjects/Entities/Entity")]
public class Entity_SO : ScriptableObject
{
    [Header("Health")]
    public float maxHealth;
    public float currentHealth;

    [Header("Movement")]
    public float moveSpeed;

    public List<Weapon_SO> weaponsInv;
    public Weapon_SO currentWeapon;

    public void Setup()
    {
        currentHealth = maxHealth;
    }
}
