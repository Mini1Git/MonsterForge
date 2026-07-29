using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Health_Component
{
    public bool godMode;
    
    public override void damageEntity(float damage)
    {
        if (godMode) {
            Debug.LogWarning("We are in godmode!");
            return;
        
        }
        Debug.Log($"Damaged the player for {damage}");
        base.damageEntity(damage);

        


        // UI updates
        UIManager.Instance.updateHealthUI();

    }
    public void setPlayerHealth(float health) // sets health when entering level.
    {
        currentHealth = health;
    }
    
}
