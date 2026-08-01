using UnityEngine;

public abstract class BossAttackSO : ScriptableObject
{
    public string animationTriggerName;

    public float damage = 10;


    public abstract void Execute(bool lookRight, BossAI boss);
}
