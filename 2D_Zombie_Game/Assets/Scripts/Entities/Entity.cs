using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    [Header("Movement")]
    public float moveSpeed;

    protected void Start()
    {
        currentHealth = maxHealth;
    }

    protected void Update()
    {
        if (currentHealth <= 0) Die();
    }

    public void damage(float damageValue)
    {
        currentHealth -= damageValue;
    }

    protected virtual void Die()
    {
        Debug.Log($"{transform.name} died!");
    }
}
