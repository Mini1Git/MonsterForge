using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerInput;
    
    public float speed = 10;
    
    [SerializeField] LayerMask LayerGround;
    
    
    [Header("Jump settings")]
    public float jumpForce = 100;
    [SerializeField] 
    private float fallMultiplier = 10;
    [SerializeField]
    private float lowJumpMultiplier = 5;

    InputAction move;
    InputAction jump;
    float extendedBoxDown = 0.2f;
    float moveDir = 0;

    Rigidbody2D rb;
    BoxCollider2D boxCollider;
    Animator animator;
    public bool isFacingRight = true;
    bool knockBack = false;
    Coroutine kb = null;
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

        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            unfreezeMovement();
        }

        jump.performed += Jump; // register the function Jump to when the "playerInput.Player.Jump" keybinding is pressed.

    }
    public void unfreezeMovement()
    {
        move.Enable();
        jump.Enable();
    }
    public void freezeMovement()
    {
        move.Disable();
        jump.Disable();
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

        if (rb.linearVelocityX == 0 || !IsGrounded()) // if ur standing still or are not grounded.
        {
            animator.SetBool("move_Bool", false);
        }
        else
        {
            animator.SetBool("move_Bool", true);
        }

    }
    
    private void FixedUpdate()
    {
        if (!knockBack)
        {
            rb.linearVelocityX = moveDir * speed;
        }
        
        
        if (rb.linearVelocityY < 0) // if falling
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        //check if jump not being held down and going up
        else if (rb.linearVelocityY > 0 && jump.ReadValue<float>() == 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    public void Jump(InputAction.CallbackContext context)
    {
        if (IsGrounded())
        {

            rb.linearVelocityY = jumpForce;
            
            
        }
        
    }
    public void Knockback(float force)
    {
        if (gameObject.activeInHierarchy)
        {
            if (kb == null)
            {
                if (isFacingRight)
                {
                    kb = StartCoroutine(KnockbackCoroutine(-force));
                }
                else
                {
                    kb = StartCoroutine(KnockbackCoroutine(force));
                }
            }
        }
    }
    private IEnumerator KnockbackCoroutine(float amount)
    {
        knockBack = true;
        
        rb.linearVelocityX = amount;
        yield return new WaitForSeconds(1f);
        knockBack = false;
        kb = null;
    }
    public bool IsGrounded()
    {
        
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, extendedBoxDown, LayerGround);
        
        return hit.collider != null;
    }

    
}
