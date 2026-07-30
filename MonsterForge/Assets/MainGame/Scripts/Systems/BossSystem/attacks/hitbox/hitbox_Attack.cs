using UnityEngine;

public class hitbox_Attack : MonoBehaviour 
{

    bool hasHit = false;
    
    public Vector2 attackBoxSize;

    public float damageAmount;
    
    
    private void Awake()
    {
        GetComponent<BoxCollider2D>().size = attackBoxSize;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        PlayerAttack playerAttack = collision.GetComponentInParent<PlayerAttack>();

        if (hasHit)
        {
            return;
        }
        hasHit = true;
        if (playerAttack.isParrying)
        {
            BossAI boss = GetComponentInParent<BossAI>(); 
            boss.changeState(new Parried_State(boss));
            GameObject.Destroy(gameObject);
            
            
        }
        else
        {
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
