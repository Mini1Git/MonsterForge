using UnityEngine;

public class Decision_State : Boss_State
{
    // if want to, can make this abstract, but for now, its fine and simple.
    
    public Decision_State(BossAI boss): base(boss)
    {
    }
    public override void EnterState()
    {
        
        if (bossAI.health.currentHealth < bossAI.healthTolerance && !bossAI.healed)
        {
            bossAI.healed = true;
            bossAI.changeState(new Heal_State(bossAI));
            return;
        }
        float currentDistance = Mathf.Abs(bossAI.player.transform.position.x - bossAI.transform.position.x);
        
        float randoNum = Random.value;
        if (currentDistance > bossAI.maxDistanceToPlayer)
        {
            bossAI.changeState(new MoveToPlayer_State(bossAI)); // Basically, if the distance is a bit far, move the boss closer.
            return;
        }

        else if (randoNum < 0.7f)
        {
            bossAI.changeState(new Attack_State(bossAI));
            return;
        }
        else
        {
            bossAI.changeState(new Defend_State(bossAI));
            return;
        }
    }

   
}
