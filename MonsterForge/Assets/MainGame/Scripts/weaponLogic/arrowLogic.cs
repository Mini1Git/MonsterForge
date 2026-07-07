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
        if (pm.flipped)
        {
            goRight = false;
        }

        if (!hit)
        {
            if (goRight)
            {
                arrowFly.linearVelocity += Vector2.right * 10;
            }
            else
            {
                arrowFly.linearVelocity -= Vector2.right * 10;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        arrowFly.linearVelocityX = 0;
        arrowFly.linearVelocityY = 0;
        hit = true;
        this.GetComponent<BoxCollider2D>().isTrigger = false;
        if (collision.CompareTag("dummy"))
        {
            collision.GetComponent<Health_Component>().damageEntity(1);
        }
    }




}
