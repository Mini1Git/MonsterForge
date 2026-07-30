using UnityEngine;
using UnityEngine.Timeline;

public class hitbox_Spawner : MonoBehaviour
{
    [SerializeField]
    BossAI boss;
    [SerializeField]
    hitbox_Attack hitbox;
    Melee_BossAttackSO meleeAttack;

    private void Awake()
    {
        boss = GetComponentInParent<BossAI>();
        
    }
    public void Start()
    {
        
    }
    public void spawnHitbox()
    {
        meleeAttack = (Melee_BossAttackSO)boss.currentBossAttack;
        if (meleeAttack != null)
        {

            Vector2 attackPosition;

            if (boss.facingRight_Bool)
            {
                attackPosition = (Vector2)boss.transform.position + meleeAttack.attackOffset;
            }
            else
            {
                attackPosition = new Vector2(boss.transform.position.x - meleeAttack.attackOffset.x, boss.transform.position.y + meleeAttack.attackOffset.y);

            }

            hitbox.attackBoxSize = meleeAttack.attackBoxSize;
            hitbox.transform.position = attackPosition;
            hitbox.damageAmount = meleeAttack.damage;
            Instantiate(hitbox, transform, true);

        }
    }


}
