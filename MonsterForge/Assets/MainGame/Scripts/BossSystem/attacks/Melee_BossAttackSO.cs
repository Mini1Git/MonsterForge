using UnityEngine;

[CreateAssetMenu(fileName = "Melee_BossAttackSO", menuName = "Boss Attacks/Melee/Melee_BossAttackSO")]
public class Melee_BossAttackSO : BossAttackSO {

    public Vector2 attackOffset;
    public Vector2 attackBoxSize;
    [HideInInspector]
    public Vector2 gizmoAttackOffset;

    public override void Execute(bool lookRight, BossAI boss)
    {

        Debug.Log("EXECUTED BOSS ATTACK");
        Vector2 attackPos;
        if (lookRight)
        {
            attackPos = new Vector2(boss.transform.position.x, boss.transform.position.y) + new Vector2(attackOffset.x, attackOffset.y);
            gizmoAttackOffset = new Vector2(attackOffset.x, attackOffset.y);
        }
        else
        {
            attackPos = new Vector2(boss.transform.position.x, boss.transform.position.y) + new Vector2(attackOffset.x * -1, attackOffset.y);
            gizmoAttackOffset = new Vector2(attackOffset.x * -1, attackOffset.y);
        }

        
        Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPos, attackBoxSize, 0f);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.Log("Player has been hit!");
            }
        }
    }

}
