using UnityEngine;

public class Health_Component : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float maxHealth = 100;
    [SerializeField] private float _currentHealth;
    [SerializeField] private bool _dead;
    //get means read only, set is when you are writing to the value.
    
    public float currentHealth
    {

        get => _currentHealth;
    }
    public bool isDead
    {
        get => _dead; // read only
    }
    public void Awake()
    {
        _currentHealth = maxHealth;
    }


    private void Start()
    {

    }
    public virtual void damageEntity(float damage)
    {
        _currentHealth -= damage;
        if (_currentHealth <= 0)
        {
            _dead = true;
            Debug.Log(gameObject.name + " Has died!");
            gameObject.SetActive(false);
        }
    }
}
