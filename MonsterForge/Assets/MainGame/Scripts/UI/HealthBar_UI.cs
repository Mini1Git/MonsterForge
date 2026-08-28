using UnityEngine;
using UnityEngine.UI;

public class HealthBar_UI : MonoBehaviour
{
    //this is generic indiviual healthbars for entity and player.
    //checked, it should be fine.
    
    [SerializeField]
    private GameObject entity;
    [SerializeField]
    private float entity_health;
    private Slider hpBar;
    public bool isBossHealthBar = false;
    public bool isPlayer;
    
    private void Awake()
    {
        if (isPlayer)
        {
            isBossHealthBar = false;
            entity = GameObject.FindGameObjectWithTag("Player");
        }
        if (entity != null)
        {
            entity_health = entity.GetComponent<Health_Component>().currentHealth;


            hpBar = GetComponentInChildren<Slider>();
            hpBar.value = entity_health / entity.GetComponent<Health_Component>().maxHealth;
        }
        else
        {
            Debug.Log("NULL found healtbar");
        }
    }

    public void updateHealth() // this will update, the health in the hp bar.
    {

        if (isPlayer)
        {
            
            //setup player
            entity = GameObject.FindGameObjectWithTag("Player"); // because the player doesn't persists.

            entity_health = entity.GetComponent<PlayerHealth>().currentHealth;

            hpBar.value = entity_health / entity.GetComponent<PlayerHealth>().maxHealth;    
            //entity health goes negative.
            if (entity_health <= 0) // so the hp bar looks empty.
            {
                hpBar.value = 0;
                hpBar.fillRect.gameObject.SetActive(false);
            }
            else
            {
                hpBar.fillRect.gameObject.SetActive(true);
            }
                return;
        }
        entity_health = entity.GetComponent<Health_Component>().currentHealth;
        hpBar.value = entity_health/entity.GetComponent<Health_Component>().maxHealth;
        //entity health goes negative.
        

        
        
        
    }
}
