using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Player_SO player;
    public Image weaponSprite;
    public intSO currentWave;

    public TMP_Text health;
    public TMP_Text ammo;
    public TMP_Text waveText;


    // Update is called once per frame
    void Update()
    {
        health.text = $"{player.currentHealth}/{player.maxHealth}";
        ammo.text = $"{player.currentWeapon.ammoInMag}/{player.currentWeapon.magSize}";
        weaponSprite.sprite = player.currentWeapon.sprite;
        waveText.text = currentWave.value.ToString();
    }
}
