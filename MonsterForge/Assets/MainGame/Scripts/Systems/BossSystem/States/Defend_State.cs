using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Defend_State : Boss_State
{
    public Defend_State(BossAI boss): base(boss)
    {

    }
    public override void EnterState()
    {
        bossAI.animator.SetBool("moving", true);
        
    }

    public override void ExitState()
    {
        Debug.Log("Done Defending!");
    }

    public override void UpdateState()
    {


        bossAI.Defend(); // ok basically, we'll want to move away from the player.
        float distance = Mathf.Abs(bossAI.transform.position.x - bossAI.player.transform.position.x);
        if (distance > bossAI.maxDistanceToPlayer*3)
        {
            
            bossAI.changeState(new Decision_State(bossAI));
        }
    }
    
    
}
