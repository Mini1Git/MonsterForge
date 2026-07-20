using UnityEngine;

public class Attack_State : Boss_State
{
    private BossAttackSO attackSO;

    public Attack_State(BossAI boss, BossAttackSO attack) : base(boss) // The base(boss) calls the parent constructor.
    {
        
        attackSO = attack;
    }

    public override void EnterState()
    {
        //so then from what i understand, best to use animationEvents. Public functions in BossTest, animations in states
        //bossAI.animator.Play(attackSO.animationStateName);
        bossAI.animator.SetTrigger(attackSO.animationTriggerName);


    }

    public override void ExitState()
    {

        attackSO = null;
        
    }
}

