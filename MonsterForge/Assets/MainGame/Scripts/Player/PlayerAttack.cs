using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{   
    
    public float attackDamage = 0;
    public Weapon_SO weapon;
    public Transform attackRef;
    public Vector2 attackSize = Vector2.one;

    Weapons_Picker weaponPicker;
    PlayerInput playerInput;
    InputAction attack;
    bool armed = false;
    
    public LayerMask layersEnemies;
    Animator animator;
    
    public AnimatorOverrideController[] animatorOverrideControllers; // need to use.
    

    private void Awake()
    {

        
        playerInput = new PlayerInput();

    }
    void Start()
    {
        weaponPicker = GameObjectManager.Instance.weaponContainer.GetComponent<Weapons_Picker>();
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
        if (weaponPicker.weaponEquipped != null && !armed)
        {
            armed = true;
            weapon = weaponPicker.weaponEquipped;
            attackDamage = weapon.damage;
            animator.runtimeAnimatorController = weapon.animatorController;

            
        }
    }

    public void startAttack(InputAction.CallbackContext context)
    {
        if (armed)
        {
            animator.SetBool("attack_Bool", true);
        }

        
    }

    private void shootBow()
    {
        //spawn arrow prefab
        GameObject arrow = weapon.projectile;
        GameObject.Instantiate(arrow, this.attackRef.transform);
        

    }

    public void attackingLogic() // depending on what type of attack...need to change/overhaul.
    {
        if (weapon.name == "Bow")
        {
            Debug.Log("PEW");
            shootBow();
            return;
        }

        Collider2D[] hitAttack = Physics2D.OverlapBoxAll(attackRef.position, attackSize, 90, layersEnemies);

        foreach (Collider2D hit in hitAttack)
        {
            Debug.Log(hit.name);
            Health_Component entityHp = hit.GetComponent<Health_Component>();
            if (entityHp != null)
            {
                entityHp.damageEntity(attackDamage);

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
