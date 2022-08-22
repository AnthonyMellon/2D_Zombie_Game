using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon_SO currentWeapon;
    public List<Entity_SO> hitBlackList;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon.Setup();
    }   

    public void shoot(float horiz, float vert)
    {
        Debug.DrawLine(transform.position, (Vector2)transform.position + new Vector2(horiz, vert) * currentWeapon.range, Color.magenta);

        if (currentWeapon.ammoInMag <= 0) //Ensure there is ammo to shoot with
        {
            tryReload(); //Auto reload
            return;
        }
        if (!currentWeapon.roundLoaded) return; //Ensure there is a round loaded
        if (currentWeapon.reloading) return; //Ensure the weapon isnt currently reloading

        //Shot fired//
        StartCoroutine(cycleRound());

        List<RaycastHit2D> hits = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.parent.position, new Vector2(horiz, vert), currentWeapon.range));
        if (hits.Count <= 0) return; //Ensure something was actually hit
                                     
        Entity target = FindValidTarget(hits);
        if (target == null) return; //Ensure there was a valid target hit        
            
        target.Damage(currentWeapon.damage);
        //Debug.Log($"{target.self.name}: {target.self.currentHealth}/{target.self.maxHealth}");                                    
    }

    public Entity FindValidTarget(List<RaycastHit2D> candidates)
    {
        foreach(RaycastHit2D candidate in candidates)
        {
            if (candidate.transform == null) continue; //Ensure the target has a transform
            if (candidate.transform == transform.parent) continue; //Ensure candidate isnt the parent
            if (!candidate.transform.TryGetComponent(out Entity candidateEntity)) continue; //Ensure the candidate is an entity
            foreach(Entity_SO blackListHit in hitBlackList)
            {
                if (candidateEntity.self.name == blackListHit.name) continue; //Ensure the candidate isnt on the blacklist                
                return candidateEntity;                
            }                                       
        }
        return null;
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
