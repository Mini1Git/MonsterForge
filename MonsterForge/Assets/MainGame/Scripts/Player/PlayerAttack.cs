using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{   
    
    public float attackDamage = 0;
    public Transform attackRef;
    private Vector2 attackSize = Vector2.one;

    public Weapon_SO weaponEquipped;
    PlayerInput playerInput;
    InputAction attack;
    bool armed = false;
    
    public LayerMask layersEnemies;
    Animator animator;



    private void Awake()
    {

        
        playerInput = new PlayerInput();

    }
    void Start()
    {

        
        animator = GetComponent<Animator>();

        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.currentWeapon != null) // check if GameObjectManager's current weapon is null or not. persists.
            {
                EquipWeapon(GameManager.Instance.currentWeapon);
            }
        }
        



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
        if (armed)
        {
            animator.SetBool("attack_Bool", true);
        }

        
    }
    public void EquipWeapon(Weapon_SO weapon)
    {
        weaponEquipped = weapon;
        armed = true;
        animator.runtimeAnimatorController = weapon.animatorController;
        attackDamage = weapon.damage;
    }
    private void shootBow()
    {
        //spawn arrow prefab
        GameObject arrow = weaponEquipped.projectile;
        GameObject.Instantiate(arrow, this.attackRef.transform);
        

    }

    public void attackingLogic() // depending on what type of attack...need to change/overhaul.
    {
        if (weaponEquipped.attackType == Weapon_SO.AttackType.ProjectileBased)
        { //overhaul how this will work.
            Debug.Log("PEW");
            shootBow();
            return;
        }

        Collider2D[] hitAttack = Physics2D.OverlapBoxAll(attackRef.position, attackSize, 90, layersEnemies);

        foreach (Collider2D hit in hitAttack)
        {
            Debug.Log(hit.name);
            dummy_component dummy = hit?.GetComponent<dummy_component>();
            BossAI bossAI = hit?.GetComponent<BossAI>();
            if (bossAI != null)
            {
                bossAI.TakeDamage(attackDamage);
                
            }
            else if (dummy != null)
            {
                dummy.damageEntity(attackDamage);
            }
            
        }
    }
    public void endAttack() {
        Debug.Log("ENDED ATTACK");
        animator.SetBool("attack_Bool", false);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(attackRef.transform.position, attackSize);
    }
}
