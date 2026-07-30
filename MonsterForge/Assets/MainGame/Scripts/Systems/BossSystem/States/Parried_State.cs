using UnityEngine;

public class Parried_State : Boss_State
{

    //got parried
    public Parried_State(BossAI boss) : base(boss)
    {
    }

    public override void EnterState()
    {
        bossAI.player.GetComponent<PlayerAttack>().parryEffect();
        bossAI.animator.SetTrigger("parried"); // draw a stunned reaction ig.


        bossAI.hitFlash(0.2f);
    }


}
