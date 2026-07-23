using UnityEngine;

public class Heal_State : Boss_State
{
    public Heal_State(BossAI boss) : base(boss){}

    public override void EnterState()
    {
        timer = 2f;
        Debug.LogWarning("HEALING!");
        
        //canMove = false
        bossAI.animator.SetTrigger("heal");
        
    }

    public override void ExitState()//we have no exit.
    {
        Debug.Log("LEAVING HEALTH");
        //canMove = true
    }

    public override void UpdateState()
    {
        Debug.Log("HEAL UPDATE!");
        if (TimerFinished())
        {
            bossAI.changeState(new Defend_State(bossAI));
        }
    }
}
