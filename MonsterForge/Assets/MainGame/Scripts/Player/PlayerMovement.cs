using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    
    public float speed = 10;
    public float jumpForce = 100;
    [SerializeField] LayerMask LayerGround;

    InputAction move;
    InputAction jump;
    float extendedBoxDown = 0.2f;
    float moveDir = 0;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;
    
    bool isFacingRight = true;
    
    private void Awake()
    {
        
        playerInput = new PlayerInput();
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = this.GetComponent<Rigidbody2D>();
        boxCollider = this.GetComponent<BoxCollider2D>();
    }
    private void OnEnable()
    {
        move = playerInput.Player.Move;
        jump = playerInput.Player.Jump;

        move.Enable();
        jump.Enable();

        jump.performed += Jump; // register the function Jump to when the "playerInput.Player.Jump" keybinding is pressed.

    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
    public void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
    }

    void Update()
    {
        
        
        moveDir = move.ReadValue<float>();
        if (moveDir < 0 && isFacingRight) // if facing right and go left.
        {
            Flip();
        }
        else if (moveDir > 0 && !isFacingRight) // else
        {
            Flip();
        }


        //debug check if grounded.
        Debug.DrawRay(boxCollider.bounds.center + new Vector3(boxCollider.bounds.extents.x - 0.1f, 0), Vector2.down * (boxCollider.bounds.extents.y + extendedBoxDown), Color.blue); //right
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x - 0.1f, 0), Vector2.down * (boxCollider.bounds.extents.y + extendedBoxDown), Color.blue); // left
        Debug.DrawRay(boxCollider.bounds.center - new Vector3(boxCollider.bounds.extents.x, boxCollider.bounds.extents.y + extendedBoxDown), Vector2.right * (boxCollider.bounds.extents.x * 2f), Color.blue); // bottom

    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveDir * speed;
        if (rb.linearVelocityX == 0)
        {
            animator.SetBool("move_Bool", false);
        }
        else {
            animator.SetBool("move_Bool", true);
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {
            Debug.Log("JUMP");
        
            rb.linearVelocityY = jumpForce;
            
        }
        

    }

    public bool IsGrounded()
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extendedBoxDown, LayerGround);
        
        return hit.collider != null;
    }

    
}
