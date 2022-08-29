using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Kisok", menuName = "ScriptableObjects/Kiosks/Kiosk")]
public class Kiosk_SO : ScriptableObject
{
    public List<KioskItem_SO> Inventory;
}
