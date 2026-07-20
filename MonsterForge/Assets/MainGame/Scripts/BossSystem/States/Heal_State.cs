using UnityEngine;

public class Heal_State : Boss_State
{
    public Heal_State(BossAI boss) : base(boss){}

    public override void EnterState()
    {
        Debug.LogWarning("HEALING!");
    }

    public override void ExitState()
    {
        Debug.Log("LEAVING HEALTH");
    }

    public override void UpdateState()
    {
        throw new System.NotImplementedException();
    }
}
