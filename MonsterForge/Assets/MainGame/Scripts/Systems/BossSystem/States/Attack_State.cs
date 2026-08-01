using UnityEngine;

public class Attack_State : Boss_State
{

    public Attack_State(BossAI boss) : base(boss) // The base(boss) calls the parent constructor.
    {
        // chooses random attack in the list and sets the boss's current attack to it. Thus, the bossAI doesn't need to decide what attack to use, it just uses an attack.
        if (bossAI.bossAttacks.Length > 0)
        {

            bossAI.currentBossAttack = bossAI.bossAttacks[Random.Range(0, bossAI.bossAttacks.Length)];
        }
        else
        {
            Debug.LogError($"THERE ARE NO ATTACKS IN THE BOSS {bossAI.name} ATTACK LIST!");
        }
    }

    public override void EnterState()
    {
        //so then from what i understand, best to use animationEvents. Public functions in BossTest, animations in states
        //bossAI.animator.Play(attackSO.animationStateName);
        bossAI.animator.SetTrigger(bossAI.currentBossAttack.animationTriggerName);


    }

    public override void ExitState()
    {

        
        
    }
}

