using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KisokItem", menuName = "ScriptableObjects/Kiosks/KioskItem")]
public class KioskItem_SO : ScriptableObject
{
    public Weapon_SO weapon;
    public float cost;
}
