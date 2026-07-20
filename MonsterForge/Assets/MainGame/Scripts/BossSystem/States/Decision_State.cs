using UnityEngine;

public class Decision_State : Boss_State
{
    // if want to, can make this abstract, but for now, its fine and simple.
    
    public Decision_State(BossAI boss): base(boss)
    {
    }
    public override void EnterState()
    {
        
        if (bossAI.health.currentHealth < 40 && !bossAI.healed)
        {
            bossAI.healed = true;
            bossAI.changeState(new Heal_State(bossAI));
            return;
        }
        float distance = Mathf.Abs(bossAI.player.transform.position.x - bossAI.transform.position.x);
        
        float randoNum = Random.value;
        if (distance > 5)
        {
            bossAI.changeState(new MoveToPlayer_State(bossAI));
            return;
        }

        else if (randoNum < 0.7f)
        {// chooses random attack in the list and sets the boss's current attack to it. Thus, the bossAI doesn't need to decide what attack to use, it just uses an attack.
            if (bossAI.bossAttacks.Length > 0)
            {
                
                BossAttackSO currentAttack = bossAI.bossAttacks[Random.Range(0, bossAI.bossAttacks.Length)];

                bossAI.currentBossAttack = currentAttack;
                bossAI.changeState(new Attack_State(bossAI, currentAttack));
                return;
            }
            else
            {
                Debug.LogError($"THERE ARE NO ATTACKS IN THE BOSS {bossAI.name} ATTACK LIST!");
            }
        }
        else
        {
            bossAI.changeState(new Defend_State(bossAI));
            return;
        }
    }

   
}
