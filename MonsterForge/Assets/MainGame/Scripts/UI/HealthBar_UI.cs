using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    //this is generic indiviual healthbars for entity.
    
    [SerializeField]
    private GameObject entity;
    private float entity_health;
    private Slider hpBar;

    private void Awake()
    {
        if (entity == null) 
        {
            //we assume that the one missing is the player
            entity = GameObject.FindGameObjectWithTag("Player");
        }
        entity_health = entity.GetComponent<Health_Component>().currentHealth;
        hpBar = GetComponentInChildren<Slider>();
        hpBar.value = entity_health / entity.GetComponent<Health_Component>().maxHealth;

    }

    public void updateHealth() // this will update, the health in the hp bar.
    {
        if (entity == null)
        {
            //we assume that the one missing is the player
            entity = GameObject.FindGameObjectWithTag("Player");
        }

        entity_health = entity.GetComponent<Health_Component>().currentHealth;
        hpBar.value = entity_health/entity.GetComponent<Health_Component>().maxHealth;
        //entity health goes negative.
        if (entity_health <= 0) // so the hp bar looks empty.
        {
            hpBar.fillRect.gameObject.SetActive(false);
        }
        
        
    }
}
