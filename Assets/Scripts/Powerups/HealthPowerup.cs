using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class HealthPowerup : Powerup
{
    public float healthToAdd;
    public override void Apply(PowerupManager target)
    {
        Health targetHealth = target.gameObject.GetComponent<Health>();
        if (targetHealth != null)
        {
            targetHealth.ApplyHealing(healthToAdd);
        }
    }

    public override void Remove(PowerupManager target)
    {
        // Don't
    }
}
