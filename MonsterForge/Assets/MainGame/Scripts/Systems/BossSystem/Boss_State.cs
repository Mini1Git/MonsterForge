

using UnityEngine;

public abstract class Boss_State
{
    protected BossAI bossAI;
    protected float timer;
    public Boss_State(BossAI boss) {
        bossAI = boss;
    }
    public abstract void EnterState(); // 
    public virtual void ExitState() { }

    public virtual void UpdateState() { } // this will be what the boss is continously doing while in this state. 
    //BossAI contains the boss's capabilities. The states decide which capability to use and when to switch states.


    protected bool TimerFinished()
    {
        timer -= Time.deltaTime;
        return timer <= 0;
    }
}
