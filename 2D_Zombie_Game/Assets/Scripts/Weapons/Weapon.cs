using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Weapon_SO currentWeapon;
    public List<Entity_SO> hitBlackList;

    public floatSO ammoInMagExt;
    public floatSO magSizeExt;

    [Header("BulletTrail")]
    public GameObject trailObject;
    public float trailLifeTime;

    // Start is called before the first frame update
    void Start()
    {
        currentWeapon.Setup();
    }

    private void Update()
    {
        UpdateExtValues();
    }

    private void UpdateExtValues()
    {
        if (ammoInMagExt)
            if (ammoInMagExt.value != currentWeapon.ammoInMag) ammoInMagExt.value = currentWeapon.ammoInMag;

        if (magSizeExt)
            if (magSizeExt.value != currentWeapon.magSize) magSizeExt.value = currentWeapon.magSize;
    }

    public void shoot(float horiz, float vert)
    {        
        float shotTheta = Mathf.Atan2(vert, horiz);
        Vector2 shotEnd = new Vector2(currentWeapon.range * Mathf.Cos(shotTheta), currentWeapon.range * Mathf.Sin(shotTheta)) + (Vector2)transform.position;
        //Debug.DrawLine(transform.position, shotEnd, Color.magenta);

        if (currentWeapon.ammoInMag <= 0) //Ensure there is ammo to shoot with
        {
            tryReload(); //Auto reload
            return;
        }
        if (!currentWeapon.roundLoaded) return; //Ensure there is a round loaded
        if (currentWeapon.reloading) return; //Ensure the weapon isnt currently reloading

        //Shot fired//
        StartCoroutine(cycleRound());

        //Trail
        GameObject trail = Instantiate(trailObject, transform.parent);
        LineRenderer trailLine = trail.GetComponent<LineRenderer>();
        trailLine.positionCount = 2;
        trailLine.SetPosition(0, transform.parent.position);
        trailLine.SetPosition(1, shotEnd);
        Destroy(trail, trailLifeTime);

        //Raycast
        List<RaycastHit2D> hits = new List<RaycastHit2D>(Physics2D.RaycastAll(transform.parent.position, new Vector2(horiz, vert), currentWeapon.range));
        if (hits.Count <= 0) return; //Ensure something was actually hit
                                     
        Entity target = FindValidTarget(hits);
        trailLine.SetPosition(1, hits[1].point);      
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
