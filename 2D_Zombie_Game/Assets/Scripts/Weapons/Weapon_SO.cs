using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Items/Weapon")]
public class Weapon_SO : ScriptableObject
{
    [Header("Stats")]
    public float damage;
    public float range;
    [Tooltip("Number of bullets fired per second")]
    public float fireRate;
    public float magSize;
    [Tooltip("How many seconds it takes to reload")]
    public float reloadSpeed;

    [Header("Visuals")]
    public Sprite sprite;

    [HideInInspector] public bool roundLoaded = true;
    [HideInInspector] public bool reloading = false;
    [HideInInspector] public float ammoInMag;

    public void Setup()
    {
        ammoInMag = magSize;
        reloading = false;
        roundLoaded = true;
    }
}
