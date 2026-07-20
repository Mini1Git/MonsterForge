using UnityEngine;

public abstract class BossAttackSO : ScriptableObject
{
    public string animationTriggerName;
    
    
    
    
    public abstract void Execute(bool lookRight, BossAI boss);
}
