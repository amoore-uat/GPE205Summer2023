using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth = 100f;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Pawn source)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, minHealth, maxHealth);
        Debug.Log(source.name + " did " + damage + " damage to " + gameObject.name);
        if (Mathf.Approximately(currentHealth, minHealth))
        {
            Die(source);
        }
    }
    
    public void ApplyHealing(float value)
    {
        if (value < 0)
        {
            Debug.LogWarning("Attempted to heal for negative amount");
            return;
        }
        currentHealth = Mathf.Clamp(currentHealth + value, minHealth, maxHealth);
    }

    private void Die(Pawn source)
    {
        Destroy(gameObject);
    }
}
