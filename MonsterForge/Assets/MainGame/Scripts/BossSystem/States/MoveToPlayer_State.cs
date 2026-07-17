using UnityEngine;

public class MoveToPlayer_State : Boss_State
{
    public MoveToPlayer_State(BossAI boss) : base(boss)
    {

    }

    public override void EnterState()
    {
        timer = 1f; // more so close the distance instead of timer.
        Debug.Log("MOVING TOWARDS PLAYER");
    }

    public override void ExitState()
    {
        Debug.Log("Exit moving!");
    }

    public override void UpdateState()
    {
        
        float distance = Mathf.Abs( bossAI.transform.position.x - bossAI.player.transform.position.x); // horizontal
        

        if (distance <= 3f)
        {
            bossAI.changeState(new Attack_State(bossAI));
        }
        else
        {
            bossAI.Move();
        }
    }
}
