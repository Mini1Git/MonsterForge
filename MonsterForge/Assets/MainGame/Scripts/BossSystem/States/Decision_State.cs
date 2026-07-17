using UnityEngine;

public class Decision_State : Boss_State
{
    // if want to, can make this abstract, but for now, its fine and simple.
    public Decision_State(BossAI boss): base(boss)
    {
    }
    public override void EnterState()
    {
        
        float distance = Mathf.Abs(bossAI.player.transform.position.x - bossAI.transform.position.x);
        Debug.Log(distance);
        float randoNum = Random.value;
        if (distance > 5)
        {
            bossAI.changeState(new MoveToPlayer_State(bossAI));
        }

        else if (randoNum < 0.5f)
        {
            bossAI.changeState(new Attack_State(bossAI));
        }
        else
        {
            bossAI.changeState(new Defend_State(bossAI));
        }
    }

   
}
