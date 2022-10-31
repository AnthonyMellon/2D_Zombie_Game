using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public bool sharedWeapon;

    public Weapon_SO currentWeapon;
    public List<Entity_SO> hitBlackList;
    [SerializeField]private string myLayer;

    public floatSO ammoInMagExt;
    public floatSO magSizeExt;

    [Header("BulletTrail")]
    public GameObject trailObject;
    public float trailLifeTime;

    [Header("Sounds")]
    public AudioSource shootSound;


    // Start is called before the first frame update
    void Start()
    {
        if(sharedWeapon) currentWeapon = Instantiate(currentWeapon);
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

    public bool shoot(float horiz, float vert) //Returns true if something was hit
    {        
        float shotTheta = Mathf.Atan2(vert, horiz);
        Vector2 shotEnd = new Vector2(currentWeapon.range * Mathf.Cos(shotTheta), currentWeapon.range * Mathf.Sin(shotTheta)) + (Vector2)transform.position;
        //Debug.DrawLine(transform.position, shotEnd, Color.magenta);

        if (currentWeapon.ammoInMag <= 0) //Ensure there is ammo to shoot with
        {
            tryReload(); //Auto reload
            return false;
        }
        if (!currentWeapon.roundLoaded) return false; //Ensure there is a round loaded
        if (currentWeapon.reloading) return false; //Ensure the weapon isnt currently reloading

        //Shot fired//
        shootSound.Play();
        StartCoroutine(cycleRound());

        //Trail
        GameObject trail = Instantiate(trailObject, transform);
        LineRenderer trailLine = trail.GetComponent<LineRenderer>();
        trailLine.positionCount = 2;
        trailLine.SetPosition(0, transform.position);
        trailLine.SetPosition(1, shotEnd);
        Destroy(trail, trailLifeTime);

        //Raycast        
        RaycastHit2D hit = Physics2D.Raycast(transform.position, new Vector2(horiz, vert), currentWeapon.range, ~(LayerMask.GetMask(myLayer)));
        if (hit && hit.transform.TryGetComponent(out Entity hitEntity)) hitEntity.Damage(currentWeapon.damage);

        return true;

        //Debug.Log($"{target.self.name}: {target.self.currentHealth}/{target.self.maxHealth}");                                    
    }

    public Entity FindValidTarget(List<RaycastHit2D> candidates)
    {
        foreach(RaycastHit2D candidate in candidates)
        {
            if (candidate.transform == null) continue; //Ensure the target has a transform
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
