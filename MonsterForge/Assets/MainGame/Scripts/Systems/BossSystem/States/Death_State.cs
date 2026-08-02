using UnityEngine;

public class Death_State : Boss_State
{
    public Death_State(BossAI boss) : base(boss)
    {
    }

    public override void EnterState()
    {
        Debug.Log("BOSS STATE: Boss died!");
        //death animation
        bossAI.animator.SetTrigger("DEATH");
        bossAI.spawnBossSoul();

    }
    
}
