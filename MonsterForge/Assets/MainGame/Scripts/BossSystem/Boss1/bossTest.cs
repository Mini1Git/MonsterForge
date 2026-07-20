using UnityEngine;

public class bossTest : BossAI
{
    
    
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

        currentBossAttack?.Execute(facingRight(), this);

    }

    public override void Defend() // should be some sort of punish maybe? 
    {
        Debug.Log("DEFEND!");
    }
    public override void Move()
    {
        
        Vector3 direction = new Vector3((player.transform.position.x - transform.position.x), 0,0);
        //moves horizontally to close distance to player.
        transform.position += direction * moveSpeed * Time.deltaTime;

    }

    private void OnDrawGizmos()
    {
        if (currentBossAttack == null)
        {
            return;
        }

        if (currentBossAttack is Melee_BossAttackSO)
        {
            Melee_BossAttackSO melee_BossAttack = (Melee_BossAttackSO) currentBossAttack;
            Gizmos.color = Color.red;
            

            Gizmos.DrawWireCube(transform.position+(Vector3)melee_BossAttack.gizmoAttackOffset, melee_BossAttack.attackBoxSize);
        }
        
    }




}
