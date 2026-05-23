using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputAction playerMovement;
    public float speed = 10;

    Rigidbody2D rb;
    Vector2 moveDir = Vector2.zero;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        if (playerMovement != null){ 
            playerMovement.Enable(); 
        }
    }
    private void OnDisable()
    {
        playerMovement.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        moveDir = playerMovement.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = moveDir * speed;
    }
}
