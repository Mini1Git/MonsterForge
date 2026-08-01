using UnityEngine;

[CreateAssetMenu(fileName = "Melee_BossAttackSO", menuName = "Boss Attacks/Melee/Melee_BossAttackSO")]
public class Melee_BossAttackSO : BossAttackSO {
    
    public Vector2 attackOffset;
    public Vector2 attackBoxSize;
    public Sprite attackFrame;
    

    public override void Execute(bool lookRight, BossAI boss)
    {
        Vector2 attackPosition;

        if (lookRight)
        {
            attackPosition = (Vector2)boss.transform.position + attackOffset;
        }
        else
        {
            attackPosition = new Vector2(boss.transform.position.x - attackOffset.x, boss.transform.position.y + attackOffset.y);
            
        }
       
       
    }

}
