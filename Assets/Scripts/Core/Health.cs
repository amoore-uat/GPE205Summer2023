using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealthChanged : UnityEvent<float,float>
{

}


public class Health : MonoBehaviour
{
    private const float minHealth = 0f;
    public float currentHealth;
    public float maxHealth = 100f;
    public HealthChanged OnHealthChanged = new HealthChanged();

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damage, Pawn source)
    {
        OnHealthChanged.Invoke(currentHealth,maxHealth);
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
        // Get the player index if killed by a player
        int playerIndex = GameManager.Instance.GetPlayerIndex(source);
        // Award points to that player
        if (playerIndex != -1)
        {
            GameManager.Instance.points[playerIndex] += source.pointsOnKilled;
        }
        Destroy(gameObject);
    }
}
