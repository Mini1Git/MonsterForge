using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public PlayerInput playerMovement;
    InputAction move;
    InputAction jump;

    public float speed = 10;
    public float jumpForce = 100;
    Rigidbody2D rb;
    float moveDir = 0;
    private void Awake()
    {
        playerMovement = new PlayerInput();
    }
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
    }
    private void OnEnable()
    {
        move = playerMovement.Player.Move;
        jump = playerMovement.Player.Jump;

        move.Enable();
        jump.Enable();

        jump.performed += Jump; // register the function Jump to when the "playerMovement.Player.Jump" keybinding is pressed.

    }
    private void OnDisable()
    {
        move.Disable();
        jump.Disable();
    }
    // Update is called once per frame
    void Update()
    {
        moveDir = move.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocityX = moveDir * speed;
    }

    public void Jump(InputAction.CallbackContext context)
    {
        Debug.Log("JUMP");
        
        rb.linearVelocityY = jumpForce;
    }
}
