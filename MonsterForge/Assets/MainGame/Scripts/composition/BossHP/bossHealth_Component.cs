using UnityEngine;
using System;

public class bossHealth_Component : Health_Component
{
    public event Action onBossDie;

    public override void die() // if the boss dies, he will send a signal to GameManager.
    {
        
        onBossDie?.Invoke();
        base.die();

        

    }


}
