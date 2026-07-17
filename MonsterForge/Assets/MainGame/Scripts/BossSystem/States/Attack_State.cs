using UnityEngine;

public class Attack_State : Boss_State
{
    

    public Attack_State(BossAI boss) : base(boss) // The base(boss) calls the parent constructor.
    {
        
    }

    public override void EnterState()
    {
       //so then from what i understand, best to use animationEvents. Public functions in BossTest, animations in states
        bossAI.animator.SetBool("attack", true);
        timer = 1f;
        bossAI.Attack();

        
    }

    public override void ExitState()
    {
        Debug.Log("Exiting Attack State!");     
    }

    public override void UpdateState()
    {
       
        if (TimerFinished())
        {
            bossAI.animator.SetBool("attack", false);
            bossAI.changeState(new Decision_State(bossAI));
        }
    }
}

