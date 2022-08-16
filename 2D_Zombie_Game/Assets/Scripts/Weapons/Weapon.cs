using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon_SO currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon.Setup();
    }   

    public void shoot(float horiz, float vert)
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(horiz, vert) * currentWeapon.range, Color.magenta);

        if(currentWeapon.ammoInMag > 0) //Is there ammo available to shoot with?
        {
            if(currentWeapon.roundLoaded && !currentWeapon.reloading) //Do I have a round loaded and am I not in the middle or reloading?
            {
                List<RaycastHit2D> hits = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.parent.position, new Vector2(horiz, vert), currentWeapon.range));
                if (hits[0].transform == transform.parent) hits.RemoveAt(0); //Remove self from the list of hits

                if (hits.Count > 0 && hits[0].transform != null) //Ensure whatever was hit has a transform
                {   
                    if (hits[0].transform.TryGetComponent(out Entity hitEntity))
                    {
                        hitEntity.Damage(currentWeapon.damage);
                        Debug.Log($"{hitEntity.self.name}: {hitEntity.self.currentHealth}/{hitEntity.self.maxHealth}");
                    }
                }
                StartCoroutine(cycleRound());
            }
        }
        else //No ammo left :( auto reload
        {
            tryReload();
        }                
    }

    //Cycles the round in the weapon
    private IEnumerator cycleRound()
    {
        currentWeapon.ammoInMag -= 1;
        currentWeapon.roundLoaded = false;
        yield return new WaitForSeconds(1 / currentWeapon.fireRate);
        currentWeapon.roundLoaded = true;
    }

    public void tryReload()
    {      
        if (!currentWeapon.reloading && currentWeapon.ammoInMag < currentWeapon.magSize) //Not reloading currently and missing ammo in mag
        {
            StartCoroutine(reload());
        }
    }
    private IEnumerator reload()
    {
        currentWeapon.reloading = true;
        yield return new WaitForSeconds(currentWeapon.reloadSpeed);
        currentWeapon.ammoInMag = currentWeapon.magSize;
        currentWeapon.reloading = false;        
    }
}
