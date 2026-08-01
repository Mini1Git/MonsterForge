using UnityEngine;

public class hitbox_Attack : MonoBehaviour 
{

    public bool hasHit = false;
    
    public Vector2 attackBoxSize;

    public float damageAmount;
    
    
    private void Awake()
    {
        GetComponent<BoxCollider2D>().size = attackBoxSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;


        BossAI boss = GetComponentInParent<BossAI>();
        PlayerAttack playerAttack = collision.GetComponentInParent<PlayerAttack>();

        if (hasHit)
            return;
        
        hasHit = true;
        //Debug.LogError("HIT");
        if (playerAttack.parryTiming == PlayerAttack.parry_Timing.Perfect || playerAttack.parryTiming == PlayerAttack.parry_Timing.Late)
        {
            
            boss.changeState(new Parried_State(boss));
            GameObject.Destroy(gameObject);
            
            
        }
        else
        {
            Debug.LogWarning("FAILED TO PARRY!");
            boss.player.GetComponent<PlayerAttack>().failParry();
            PlayerHealth playerHealth = collision.GetComponentInParent<PlayerHealth>(); // gets collision component
            
            playerHealth.damageEntity(damageAmount);
        }
        
    }

    



    public void setHitboxActive(bool hitboxActive)
    {
        if (hitboxActive)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
