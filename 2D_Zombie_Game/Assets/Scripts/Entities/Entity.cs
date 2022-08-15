using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField]
    protected Entity_SO self;

    protected void Start()
    {
        self.Setup();
    }

    protected void Update()
    {
        if (self.currentHealth <= 0) Die();
    }

    public void Damage(float damageValue)
    {
        self.currentHealth -= damageValue;
    }

    protected virtual void Die()
    {
        //Debug.Log($"{transform.name} died!");
    }
}
