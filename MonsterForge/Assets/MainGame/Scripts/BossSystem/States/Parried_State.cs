using UnityEngine;

public class Parried_State : Boss_State
{

    //got parried
    public Parried_State(BossAI boss) : base(boss)
    {
    }

    public override void EnterState()
    {
        Debug.LogWarning($"GOD PARRY from {bossAI.currentBossAttack}");
        bossAI.animator.Play("Parried");
        
    }


}
