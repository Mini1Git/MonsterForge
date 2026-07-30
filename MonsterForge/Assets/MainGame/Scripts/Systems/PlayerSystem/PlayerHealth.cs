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
        
        base.damageEntity(damage);

        


        

    }
    public void setPlayerHealth(float health) // sets health when entering level.
    {
        currentHealth = health;
    }
    
}
