using UnityEngine;

public class Parried_State : Boss_State
{

    
    public Parried_State(BossAI boss) : base(boss)
    {
    }

    public override void EnterState()
    {
        PlayerAttack parry = bossAI.player.GetComponent<PlayerAttack>();
        PlayerHealth playerHealth = bossAI.player.GetComponent<PlayerHealth>();
        PlayerMovement pm = bossAI.player.GetComponent <PlayerMovement>();
        if (parry.parryTiming == PlayerAttack.parry_Timing.Perfect)
        {
            
            parry.perfectParryEffect();
            bossAI.Parried();
            //push boss back slightly...
            bossAI.hitFlash(0.2f);
            bossAI.animator.SetTrigger("parried");
        }
        else if (parry.parryTiming == PlayerAttack.parry_Timing.Late)
        {
            parry.lateParryEffect();
            Debug.Log($"{bossAI}: {bossAI.currentBossAttack}");
            playerHealth.damageEntity(bossAI.currentBossAttack.damage * 0.5f);
            pm.Knockback(5);
            //Debug.LogError("Reduced damage!");
        }
        


        
    }


}
