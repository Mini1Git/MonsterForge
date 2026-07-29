using UnityEngine;
using UnityEngine.Timeline;

public class meleeAttackDEBUG : MonoBehaviour
{
    public Melee_BossAttackSO bossAttack;
    [Tooltip("Click this button to check out the attack frame for this boss attack!")]
    public bool attackFramePreview;
    BossAI boss;
    SpriteRenderer spriteRenderer;
    [Header("Debug Warnings")]
    [SerializeField]
    bool bossAttackWarning = false;
    [SerializeField]
    bool bossAIWarning = false;
    [SerializeField]
    bool spriteRendererWarning = false;
    private void OnDrawGizmos()
    {// ondrawGizmo runs even in editor. so must put any refs here.

        if (boss == null)
        {
            boss = GetComponentInParent<BossAI>();
        }


        if (spriteRenderer == null) {
            spriteRenderer = GetComponentInParent<SpriteRenderer>();
        }


        if (bossAttack == null) 
        {
            if (!bossAttackWarning)
            {
                Debug.LogWarning("Boss Attack is missing!");
                bossAttackWarning = true;
            }
            return;
        }
        if (boss == null)
        {
            if (!bossAIWarning)
            {
                Debug.LogWarning("Boss in parent is missing!");
                bossAIWarning = true;
            }
            return;
        }
        if (spriteRenderer == null)
        {
            if (!spriteRendererWarning)
            {
                Debug.LogWarning("Sprite Renderer in parent is missing!");
                spriteRendererWarning = true;
            }
            return;
        }
        //reset the bools
        bossAttackWarning = false;
        spriteRendererWarning = false;
        bossAIWarning = false;

        if (attackFramePreview)
        {
            if (bossAttack.attackFrame == null)
            {
                Debug.LogError($"Attack frame in {bossAttack} is missing! Please add one!");
                return;
            }
            spriteRenderer.sprite = bossAttack.attackFrame;
            attackFramePreview = false;
        }
        
        Gizmos.color = Color.red;
        if (boss.facingRight_Bool)
        {
            Gizmos.DrawWireCube((Vector2)boss.transform.position + bossAttack.attackOffset, bossAttack.attackBoxSize);
        }
        else
        {
            Gizmos.DrawWireCube(new Vector2(boss.transform.position.x - bossAttack.attackOffset.x, boss.transform.position.y + bossAttack.attackOffset.y), bossAttack.attackBoxSize);

        }

    }
}
