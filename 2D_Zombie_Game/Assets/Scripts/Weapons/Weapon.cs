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

        if(currentWeapon.ammoInMag > 0) //Is there ammo available to shoot with?
        {
            if(currentWeapon.roundLoaded && !currentWeapon.reloading) //Do I have a round loaded and am I not in the middle or reloading?
            {
                List<RaycastHit2D> hits = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.parent.position, new Vector2(horiz, vert), currentWeapon.range));
                
                if(hits.Count > 0) //Did I actually hit something?
                {
                    Entity target = FindValidTarget(hits);
                    if(target != null) //Was there a valid target?
                    {
                        target.Damage(currentWeapon.damage);
                        Debug.Log($"{target.self.name}: {target.self.currentHealth}/{target.self.maxHealth}");
                    }
                    StartCoroutine(cycleRound());
                }

            }
        }
        else //No ammo left :( auto reload
        {
            tryReload();
        }                
    }

    public Entity FindValidTarget(List<RaycastHit2D> candidates)
    {
        foreach(RaycastHit2D candidate in candidates)
        {
            if(candidate.transform != null) //Does the target have a transform
            {
                if (candidate.transform != transform.parent) //Ensure candidate isnt self
                {
                    if (candidate.transform.TryGetComponent(out Entity candidateEntity)) //Is the candidate an entity?
                    {
                        foreach(Entity_SO blackListHit in hitBlackList)
                        {
                            Debug.Log($"{blackListHit.name}|{candidateEntity.self.name}");
                            if(candidateEntity.self.name != blackListHit.name) //Is the entity not on the black list?
                            {
                                return candidateEntity;
                            }
                        }    
                    }
                }
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
