using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public enum parry_Timing
    {
        None,
        Perfect,
        Late
    }
    public parry_Timing parryTiming;
    [Header("Parrying Settings")]
    public bool canParry = true;
    [SerializeField]
    private float parryEffect_Duration;
    public float parryCooldown;
    public float parryWindow = 1f;
    public float perfect_parryWindowMult = 0.25f;
    public float late_parryWindowMult = 0.75f;
    public bool isParrying = false;
    public bool parrySuccess = false;
    [Header("Attack Settings")]
    public float attackDamage = 0;
    public Transform attackRef;
    public Weapon_SO weaponEquipped;
    public LayerMask layersEnemies;

    private Vector2 attackSize = Vector2.one;
    private PlayerMovement movement;
    private PlayerInput playerInput;
    private InputAction attack;
    private InputAction parry;
    private bool armed = false;
    [Header("Particle SHIELD")]
    [SerializeField]
    private ParticleSystem parryShield; // this is the actual prefab
    private ParticleSystem.MainModule mainParryShield; // this is main module so we can modify...
    public ParticleSystem parryShield_INSTANCE;//temp var to easily destroy. Instance of parryShield.
    private Animator animator;
    private Coroutine stunnedCoroutine;
    
    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        
        playerInput = new PlayerInput();
        mainParryShield = parryShield.main;

        mainParryShield.duration = parryWindow-0.1f; // this modifys the actual prefab. not instance.

    }
    void Start()
    {
        parryTiming = parry_Timing.None;
        
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
        parry = playerInput.Player.Parry;

        if (GameObject.FindGameObjectWithTag("Boss") == null)
        {
            disableAttack();
        }


        parry.performed += startParry;
        attack.performed += startAttack;
    }


    public void enableAttack()
    {
        parry.Enable();
        attack.Enable();
    }
    public void disableAttack()
    {
        parry.Disable();
        attack.Disable();
    }
    private void OnDisable()
    {
        attack.Disable();
        parry.Disable();
    }

    

    public void startParry(InputAction.CallbackContext context) // use to animation based event
    {
        
        if (!canParry)
            return;

        StartCoroutine(ParryAttack());
    }

    IEnumerator ParryAttack() 
    {
        movement.freezeMovement();
        //play parry animation here

        var parryShieldShape = parryShield.shape; // shape modifier
        
        //animation here.
        if (movement.isFacingRight)
        {

            parryShieldShape.rotation = new Vector3(parryShieldShape.rotation.x, -30, 0);
            parryShield_INSTANCE = Instantiate(parryShield, this.gameObject.transform);
        }
        else
        {
            
            parryShieldShape.rotation = new Vector3(parryShieldShape.rotation.x,-137,0);
            parryShield_INSTANCE = Instantiate(parryShield, this.gameObject.transform);
        }

        canParry = false;
        isParrying = true;
        parryTiming = parry_Timing.Perfect;
        yield return new WaitForSeconds(parryWindow * perfect_parryWindowMult);
        parryTiming = parry_Timing.Late;
        yield return new WaitForSeconds(parryWindow * late_parryWindowMult);


        


        if (!parrySuccess)
        {
            GameObject.Destroy(parryShield_INSTANCE.gameObject);
        }
        else if (parrySuccess) 
        {
            
            yield return new WaitForSeconds(parryEffect_Duration);
            GameObject.Destroy(parryShield_INSTANCE.gameObject);
        }

        parryTiming = parry_Timing.None;
        movement.unfreezeMovement();
        yield return new WaitForSeconds(parryCooldown); // some cooldown before you can parry again.
        canParry = true;
        parrySuccess = false;

        
    }
    public void perfectParryEffect()
    {
        parrySuccess = true;
        parryShield_INSTANCE.Stop(); //so even tho it stopped, it's still in the scene.
        parryShield_INSTANCE.Clear(); // clears the active particles
        
        //particle system attributes.
        ParticleSystem.MainModule parryShieldINSTANCE_MAIN = parryShield_INSTANCE.main;// MODIFY INSTANCE MAIN SETTINGS.
        ParticleSystem.EmissionModule parryShieldINSTANCE_EMISSION = parryShield_INSTANCE.emission; //MODIFY INSTANCE EMISSION MODULE.
        ParticleSystem.ShapeModule parryShieldINSTANCE_SHAPE = parryShield_INSTANCE.shape;// MODIFY INSTANCE SHAPE MODULE.
        ParticleSystem.VelocityOverLifetimeModule parryShieldINSTANCE_VELOCITYOVERLIFE = parryShield_INSTANCE.velocityOverLifetime; // MODIFY INSTANCE VELOCITY OVER TIME;
        ParticleSystem.SizeOverLifetimeModule parryShieldINSTANCE_SIZE = parryShield_INSTANCE.sizeOverLifetime;
        ParticleSystem.CollisionModule parryShieldINSTANCE_COLLISION = parryShield_INSTANCE.collision;
        //MODIFICATIONS HERE
        parryShieldINSTANCE_MAIN.duration = parryEffect_Duration;
        parryShieldINSTANCE_COLLISION.enabled = true;
        parryShieldINSTANCE_SIZE.enabled = true;
        parryShieldINSTANCE_EMISSION.rateOverTime = 500;



        parryShieldINSTANCE_MAIN.startColor = Color.aliceBlue;
        parryShieldINSTANCE_MAIN.gravityModifier = 1f;


        AnimationCurve curve = new AnimationCurve();
                    //time, scale
        curve.AddKey(0f, 2.5f);   // Large when spawned
        curve.AddKey(0.2f, 1.5f);   // Shrink quickly
        curve.AddKey(0.5f, 0.0f);   // Fade to nothing

        parryShieldINSTANCE_SIZE.size = new ParticleSystem.MinMaxCurve(1f, curve);

        //END OF MODIFICATIONS

        parryShield_INSTANCE.Play();
        
        
    }
    public void lateParryEffect()
    {
        parrySuccess = true;
        parryShield_INSTANCE.Stop(); //so even tho it stopped, it's still in the scene.
        parryShield_INSTANCE.Clear(); // clears the active particles

        //particle system attributes.
        ParticleSystem.MainModule parryShieldINSTANCE_MAIN = parryShield_INSTANCE.main;// MODIFY INSTANCE MAIN SETTINGS.
        ParticleSystem.EmissionModule parryShieldINSTANCE_EMISSION = parryShield_INSTANCE.emission; //MODIFY INSTANCE EMISSION MODULE.
        ParticleSystem.ShapeModule parryShieldINSTANCE_SHAPE = parryShield_INSTANCE.shape;// MODIFY INSTANCE SHAPE MODULE.
        ParticleSystem.VelocityOverLifetimeModule parryShieldINSTANCE_VELOCITYOVERLIFE = parryShield_INSTANCE.velocityOverLifetime; // MODIFY INSTANCE VELOCITY OVER TIME;
        ParticleSystem.SizeOverLifetimeModule parryShieldINSTANCE_SIZE = parryShield_INSTANCE.sizeOverLifetime;
        ParticleSystem.CollisionModule parryShieldINSTANCE_COLLISION = parryShield_INSTANCE.collision;
        //MODIFICATIONS HERE
        parryShieldINSTANCE_MAIN.duration = parryEffect_Duration;
        parryShieldINSTANCE_COLLISION.enabled = true;
        parryShieldINSTANCE_SIZE.enabled = true;
        parryShieldINSTANCE_EMISSION.rateOverTime = 500;



        parryShieldINSTANCE_MAIN.startColor = Color.red;
        parryShieldINSTANCE_MAIN.gravityModifier = 1f;


        AnimationCurve curve = new AnimationCurve();
        //time, scale
        curve.AddKey(0f, 2.5f);   // Large when spawned
        curve.AddKey(0.2f, 1.5f);   // Shrink quickly
        curve.AddKey(0.5f, 0.0f);   // Fade to nothing

        parryShieldINSTANCE_SIZE.size = new ParticleSystem.MinMaxCurve(1f, curve);

        //END OF MODIFICATIONS

        parryShield_INSTANCE.Play();


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
