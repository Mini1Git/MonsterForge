using UnityEngine;

public class Heal_State : Boss_State
{
    public Heal_State(BossAI boss) : base(boss){}

    public override void EnterState()
    {
        Debug.LogWarning("HEALING!");
        bossAI.Heal();
        //canMove = false
    }

    public override void ExitState()//we have no exit.
    {
        Debug.Log("LEAVING HEALTH");
        //canMove = true
    }

    public override void UpdateState()
    {
        Debug.Log("HEAL UPDATE!");
    }
}
