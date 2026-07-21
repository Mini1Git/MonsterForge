using System.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.Timeline;
using UnityEngine.XR;

/*
===============================================================================
                        BOSS AI ARCHITECTURE
===============================================================================

Overall Flow

Decision State
      |
      | Chooses next behavior based on game conditions.
      |
      +------------------------------------------------------+
      |                  |                 |                  |
      v                  v                 v                  v
 Move State        Attack State      Defend State      Heal State
      |                  |                 |                  |
      +------------------+-----------------+------------------+
                             |
                             v
                      Decision State


===============================================================================
RESPONSIBILITIES
===============================================================================

BossAI
------
The BossAI is the "body" of the boss.

Responsible for:
- Health
- Movement implementation
- Attack implementation
- Animator
- Rigidbody
- Player reference
- Boss statistics (speed, attack range, etc.)
- Changing states

The BossAI SHOULD know HOW to perform actions.

Examples:
    MoveTowardsPlayer()
    Attack()
    Defend()
    Heal()
    FacePlayer()

The BossAI SHOULD NOT decide WHICH action to perform.
That is the State's responsibility.


===============================================================================

Boss_State
----------
A state represents ONE behavior.

A boss may only be in ONE state at a time.

Every state has:

EnterState()
    Called once when entering.

UpdateState()
    Called every frame while active.

ExitState()
    Called once before leaving.


===============================================================================

Decision State
---------------
Responsible for deciding WHAT to do next.

Should inspect:

- Distance to player
- Boss HP
- Player HP (optional)
- Cooldowns
- Current phase
- Random chance
- Arena conditions

Example:

Player far away?
    -> Move State

Player close?
    -> Attack State

Boss low HP?
    -> Heal State


Decision States should NOT:
- Move the boss
- Deal damage
- Spawn hitboxes


===============================================================================

Movement States
---------------
Responsible for movement behavior.

Examples:

MoveToPlayer
Retreat
Dash
Jump

These states call BossAI movement functions.

Example:

bossAI.MoveTowardsPlayer();

When movement is complete:

bossAI.ChangeState(new Decision_State(...));


===============================================================================

Attack States
-------------
Responsible for performing attacks.

EnterState()
    Play attack animation.

UpdateState()
    Wait until attack finishes.

ExitState()
    Cleanup if needed.

Attack timing should be controlled by
Animation Events whenever possible.


===============================================================================

Animation Events
----------------

Animation drives combat timing.

Good uses:

✓ Enable sword hitbox
✓ Disable sword hitbox
✓ Spawn projectile
✓ Play footsteps
✓ Play attack sounds
✓ Notify attack finished

Avoid using timers when the timing depends
on the animation.


Example timeline:

Raise Sword
     |
     |
Enable Hitbox
     |
Swing
     |
Disable Hitbox
     |
Recover
     |
Attack Finished
     |
Decision State


===============================================================================

Timers
------

Timers are best for gameplay timing.

Good uses:

- Cooldowns
- Invulnerability duration
- Shield duration
- Heal duration
- Enrage timer

Example:

timer -= Time.deltaTime;

if(timer <= 0)
{
    ...
}


===============================================================================

Inheritance
-----------

BossAI
|
+-- KnightBoss
|
+-- MageBoss
|
+-- DragonBoss

Each boss may have:

KnightDecision_State

MageDecision_State

DragonDecision_State

Not every boss needs every state.

Example:

Knight
-------
Move
Slash
Shield

Mage
-----
Teleport
Projectile
Laser

Dragon
-------
Fly
Fire Breath
Tail Swipe


===============================================================================

Design Philosophy
-----------------

BossAI knows HOW to perform actions.

States decide WHEN those actions happen.

Animation decides EXACTLY WHEN visual gameplay
events occur.

FSM controls the flow of combat.

===============================================================================
*/

//A good BossAI should be boring.  It should mostly contain the shared skeleton that makes all bosses work.
//The interesting stuff belongs to the individual bosses.

public abstract class BossAI : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed = 10f;
    public  Animator animator;
    public bool healed = false;
    public Health_Component health;
    public BossAttackSO currentBossAttack;
    public BossAttackSO[] bossAttacks;
    [Header("Boss Behavior Settings")]
    public float healthTolerance = 50;
    public float maxDistanceToPlayer = 3;
    public Vector2 attackPos;
    public bool facingRight_Bool = false;

    protected SpriteRenderer spriteRenderer;
    protected Boss_State currentState; // only children and this class can use.
    [SerializeField]
    private string stateName;
    Coroutine hitFlashRoutine;
    Material flashMat;
    


    public virtual void Awake()
    {
        health = GetComponent<Health_Component>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashMat = spriteRenderer.material;
        
        //foreach (BossAttackSO boss_attack in bossAttacks)
        //{
        //    GameObject bossAttackGameObject = new GameObject(boss_attack.name);
        //    bossAttackGameObject.transform.parent = transform; 
        //if too lazy to add them urself lol.
        //}

    }
    public virtual void changeState(Boss_State state)
    {
        //Debug.Log($"STATE CHANGE: {currentState?.GetType().Name} -> {state.GetType().Name}");
        currentState?.ExitState(); 
        currentState = state;

        stateName = currentState.GetType().Name; // get the name of the state. 

        currentState.EnterState();
    }
    public virtual void Update()
    {
        flipSprite();
        facingRight_Bool = facingRight();
        currentState?.UpdateState();
    }

    public abstract void Attack();


    public abstract void Defend();

    public abstract void Move();


    protected bool facingRight()
    {
        if (transform.position.x >= player.transform.position.x)
        {
            //This means the boss is on the right side of the player. Which means he should be looking left.
            return false;
        }
        else
        {
            return true;
        }

    }

    protected void flipSprite()
    {
        if (facingRight()) // if this returns true, then that means that the boss needs to face right!
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    public void TakeDamage(float damage) {
        Health_Component hp = GetComponent<Health_Component>();
        hp.damageEntity(damage); // does the damage.
        if (!hp.isDead) // if the boss is not dead.
        {
            
            if (hitFlashRoutine != null) // if theres already an active hitFlash, stop the current.
            {
                StopCoroutine(hitFlashRoutine);
            }
            
            hitFlashRoutine = StartCoroutine(hitFlash());
            
        }
        else
        {

            return;
        }
    }
    public IEnumerator hitFlash()
    {
        Debug.Log("Flash!");
        flashMat.SetFloat("_hitFlashAmount", 1);
        yield return new WaitForSeconds(0.1f);
        flashMat.SetFloat("_hitFlashAmount", 0);
    }


    public void Heal() {
        health.healEntity(50);
    }
    public void finishedAttack() // animation event executed
    {

        
        if (currentState is Attack_State)
        {
            changeState(new Decision_State(this));
        }
        else
        {
            Debug.LogError("This is not an attack state.");
        }
    }
}
