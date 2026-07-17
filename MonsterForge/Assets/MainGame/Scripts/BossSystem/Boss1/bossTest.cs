using UnityEngine;

public class bossTest : BossAI
{
    public Vector2 attackOffset;
    public Vector2 attackBoxSize;

    
    public override void Awake()
    {
        base.Awake();
        
    }
    public void Start()
    {
        changeState(new Decision_State(this));
    }

    public override void Attack() // use this in animationEvent.
    {
        Vector2 attackPos;
        if (facingRight())
        {
            attackPos = new Vector2(transform.position.x, transform.position.y) + new Vector2(attackOffset.x, attackOffset.y);

        }
        else
        {
            attackPos = new Vector2(transform.position.x * -1, transform.position.y) + new Vector2(attackOffset.x * -1, attackOffset.y);
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

    public override void Defend()
    {
        Debug.Log("DEFEND!");
    }
    public override void Move()
    {
        
        Vector3 direction = new Vector3((player.transform.position.x - transform.position.x), 0,0);
        //moves horizontally to close distance to player.
        transform.position += direction * moveSpeed * Time.deltaTime;

    }


    //debug
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Vector2 hitPosition = (Vector2)transform.position + attackOffset;
        if (!facingRight())
        { // if boss is facing left.
            hitPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(attackOffset.x, attackOffset.y);

        }
        else
        {
            hitPosition = new Vector2(transform.position.x, transform.position.y) + new Vector2(attackOffset.x * -1, attackOffset.y);
        }

        Gizmos.DrawWireCube(hitPosition, attackBoxSize);
    }

}
