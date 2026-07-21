using UnityEngine;

[CreateAssetMenu(fileName = "Melee_BossAttackSO", menuName = "Boss Attacks/Melee/Melee_BossAttackSO")]
public class Melee_BossAttackSO : BossAttackSO {
    public float damage = 10;
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
        Debug.Log($"{attackPosition} is where {this.name} is attacking!");
       Collider2D[] colliders = Physics2D.OverlapBoxAll(attackPosition, attackBoxSize,0);
       foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Debug.LogWarning($"{this.name}: Hit the player!");
                collider.GetComponent<PlayerHealth>().damageEntity(damage);
            }
        }
    }

}
