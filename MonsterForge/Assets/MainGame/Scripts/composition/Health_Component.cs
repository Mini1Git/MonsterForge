using System;
using UnityEngine;

public abstract class Health_Component : MonoBehaviour
{
    public float maxHealth = 100;
    [SerializeField] protected float _currentHealth;
    [SerializeField] private bool _dead;
    //get means read only, set is when you are writing to the value.
    public event Action OnHealthUpdate;
    public float currentHealth
    {

        get => _currentHealth;
        set { 
            _currentHealth = value;
            OnHealthUpdate?.Invoke();
           
        }
    }
    public bool isDead
    {
        get => _dead; // read only
    }


    
    protected virtual void Awake()
    {
        currentHealth = maxHealth;
    }


   public virtual void healEntity(float healAmount)
    {
        currentHealth += healAmount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        UIManager.Instance.updateHealthUI();
    }
    public virtual void damageEntity(float damage)
    {
        
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log($" Damaged {this.gameObject} for {damage}, current HP: {currentHealth}");
        
        UIManager.Instance.updateHealthUI();
        if (currentHealth <= 0)
        {
            UIManager.Instance.hideHealthUI(true);
            die();
        }
        
    }
    
    public virtual void die()
    {
        
        _dead = true;
        Debug.Log(gameObject.name + " Has died!");
        
        
    }
    
}
