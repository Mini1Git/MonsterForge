using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : Health_Component
{
    [SerializeField]
    private bool godMode;

    public bool invulnerable; // basically this is i frames.
    PlayerAttack pa;
    Material playerHit_Mat;
    public event Action playerDeath;
    
    protected override void Awake()
    {
        pa = GetComponent<PlayerAttack>();
        playerHit_Mat = GetComponent<SpriteRenderer>().material;
    }
    Coroutine stunnedCoroutine;
    public override void damageEntity(float damage)
    {
        if (godMode) {
            Debug.LogWarning("We are in godmode!");
            return;
        
        }
        if (invulnerable) { 
            return; 
        }
        base.damageEntity(damage);
    }
    public override void die()
    {
        base.die();
        playerDeath?.Invoke();
        gameObject.SetActive(false);

    }
    public void playerGotHit(float damage) // gives 0.5f time of i frames.
    {
        damageEntity(damage);
        playerStunned();

    }
    public void playerStunned()
    {
        if (stunnedCoroutine == null && gameObject.activeSelf)
        {
            stunnedCoroutine = StartCoroutine(stunned());
        }

    }

    private IEnumerator stunned() 
    {
        invulnerable = true;
        pa.canParry = false;
        playerHit_Mat.SetFloat("_flashAmount", 1);
        StartCoroutine(playerFlash());
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(playerFlash());
        yield return new WaitForSeconds(0.3f);
        playerHit_Mat.SetFloat("_flashAmount", 0);
        pa.canParry = true;
        stunnedCoroutine = null;
        invulnerable = false;
    }
    private IEnumerator playerFlash()
    {
        playerHit_Mat.SetFloat("_opacity", 0);
        
        yield return new WaitForSeconds(0.1f);
        playerHit_Mat.SetFloat("_opacity", 1);
        
    }

    public void setPlayerHealth(float health) // sets health when entering level.
    {
        currentHealth = health;
    }
    
}
