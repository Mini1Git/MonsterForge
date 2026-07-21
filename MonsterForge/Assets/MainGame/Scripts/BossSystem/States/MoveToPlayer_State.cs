using UnityEngine;

public class MoveToPlayer_State : Boss_State
{
    public MoveToPlayer_State(BossAI boss) : base(boss)
    {

    }

    public override void EnterState()
    {
        bossAI.animator.SetBool("moving", true);
        
    }

    public override void ExitState()
    {
        bossAI.animator.SetBool("moving", false);
        Debug.Log("Exit moving!");
    }

    public override void UpdateState()
    {
        
        float distance = Mathf.Abs( bossAI.transform.position.x - bossAI.player.transform.position.x); // horizontal
        

        if (distance <= bossAI.maxDistanceToPlayer)
        {
            bossAI.changeState(new Decision_State(bossAI));
        }
        else
        {
            bossAI.Move();
        }
    }
}
