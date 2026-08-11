using UnityEngine;

public class PlayerDeath_State : Boss_State
{
    public PlayerDeath_State(BossAI boss) : base(boss)
    {
    }

    public override void EnterState()
    {
        //do nothing
        Debug.Log("BOSS IS DOING NOTHING!");
    }
}
