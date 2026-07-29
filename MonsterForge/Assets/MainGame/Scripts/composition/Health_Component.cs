using System;
using UnityEngine;

public abstract class Health_Component : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHealth = 100;
    [SerializeField] protected float _currentHealth;
    [SerializeField] private bool _dead;
    //get means read only, set is when you are writing to the value.
    public event Action OnHealthUpdate;
    public float currentHealth
    {

        get => _currentHealth;
        set { _currentHealth = value;  }
    }
    public bool isDead
    {
        get => _dead; // read only
    }


    
    public void Awake()
    {
        _currentHealth = maxHealth;
    }


   public virtual void healEntity(float healAmount)
    {
        _currentHealth += healAmount;
        if (_currentHealth > maxHealth)
        {
            _currentHealth = maxHealth;
        }
        updateHealth();
    }
    public virtual void damageEntity(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            die();
        }
        updateHealth();
    }
    private void updateHealth()
    {
        OnHealthUpdate?.Invoke();
        UIManager.Instance.updateHealthUI();
    }
    public virtual void die()
    {
        
        _dead = true;
        Debug.Log(gameObject.name + " Has died!");
        
    }
    
}
