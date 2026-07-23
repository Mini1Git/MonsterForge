using System.Net;
using UnityEngine;

public class bossTest : BossAI
{

    public override void Awake()
    {
        base.Awake();
        

    }
    public void Start()
    {
        
    }

    public override void Attack() // use this in animationEvent.
    {
        
        currentBossAttack?.Execute(facingRight(), this);
        
    }

    public override void Defend() // should be some sort of punish maybe? 
    {
        //so he defends while backing off...
        Vector3 direction = new Vector3((player.transform.position.x - transform.position.x), 0, 0);
        //moves horizontally to close distance to player.
        transform.position -= direction * moveSpeed/2 * Time.deltaTime;

    }
    public override void Move()
    {
        
        Vector3 direction = new Vector3((player.transform.position.x - transform.position.x), 0,0);
        //moves horizontally to close distance to player.
        transform.position += direction * moveSpeed * Time.deltaTime;

    }

    




}
