using UnityEngine;

public class Defend_State : Boss_State
{
    public Defend_State(BossAI boss): base(boss)
    {

    }
    public override void EnterState()
    {
        Debug.Log("We are defending!");
        timer = 1f;
        bossAI.Defend();
    }

    public override void ExitState()
    {
        Debug.Log("Done Defending!");
    }

    public override void UpdateState()
    {
        

        
        if (TimerFinished())
        {
            bossAI.changeState(new Decision_State(bossAI));
        }
    }
    
    
}
