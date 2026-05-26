using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public Transform attackRef;
    public Vector2 attackSize = Vector2.one;
    PlayerInput playerInput;
    InputAction attack;

    public LayerMask layersEnemies;
    Animator animator;
    

    private void Awake()
    {
        playerInput = new PlayerInput();

    }
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void OnEnable()
    {
        attack = playerInput.Player.Attack;
        
        attack.Enable();

        attack.performed += startAttack;
    }

    private void OnDisable()
    {
        attack.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startAttack(InputAction.CallbackContext context)
    {

        animator.SetBool("attackPunch_Bool", true);

        
    }

    public void attackingLogic()
    {
        Collider2D[] hitAttack = Physics2D.OverlapBoxAll(attackRef.position, attackSize, 90, layersEnemies);

        foreach (Collider2D hit in hitAttack)
        {
            Debug.Log(hit.name);
        }
    }
    public void endAttack() {
        animator.SetBool("attackPunch_Bool", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackRef.transform.position, attackSize);
    }
}
