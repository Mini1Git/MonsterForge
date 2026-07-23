using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class Defend_State : Boss_State
{
    public Defend_State(BossAI boss): base(boss)
    {

    }
    public override void EnterState()
    {
        bossAI.animator.SetBool("moving", true);
        
        timer = 2f;
    }

    public override void ExitState()
    {
        Debug.Log("Done Defending!");
    }

    public override void UpdateState()
    {


        bossAI.Defend(); // ok basically, we'll want to move away from the player.

        if (TimerFinished())
        {
            
            bossAI.changeState(new Decision_State(bossAI));
        }
    }
    
    
}
