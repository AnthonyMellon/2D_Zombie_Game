using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HUD : MonoBehaviour
{
    public Entity_SO player;

    public TMP_Text health;
    public TMP_Text ammo;

    // Update is called once per frame
    void Update()
    {
        health.text = $"{player.currentHealth}/{player.maxHealth}";
        
    }
}
