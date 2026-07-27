using System.Collections;
using UnityEngine;

public class arrowLogic : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    Rigidbody2D arrowFly;
    bool hit = false;
    bool goRight = true;
    void Start()
    {
        arrowFly = GetComponent<Rigidbody2D>();
        transform.parent = null;
        PlayerMovement pm = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        if (!pm.isFacingRight)
        {
            goRight = false;
        }

        if (!hit)
        {
            if (goRight)
            {
                arrowFly.linearVelocity += Vector2.right * GameObjectManager.Instance.arrowSpeed;
            }
            else
            {
                arrowFly.linearVelocity -= Vector2.right * GameObjectManager.Instance.arrowSpeed;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("dummy") || collision.CompareTag("Wall+Floor") || collision.CompareTag("Boss")) { 

            arrowFly.linearVelocityX = 0;
            arrowFly.linearVelocityY = 0;
            hit = true;
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<BoxCollider2D>().enabled = false;
            if (collision.CompareTag("dummy"))
            {
                collision.GetComponent<Health_Component>().damageEntity(1);
            }
            else if (collision.CompareTag("Boss"))
            {
                collision.GetComponent<BossAI>().TakeDamage(GameObjectManager.Instance.arrowDamage);
                GameObject.Destroy(gameObject);
                return;
            }

        }
        StartCoroutine(Despawn());
    }

    IEnumerator Despawn()
    {

        yield return new WaitForSeconds(4);
        GameObject.Destroy(gameObject);
    }



}
