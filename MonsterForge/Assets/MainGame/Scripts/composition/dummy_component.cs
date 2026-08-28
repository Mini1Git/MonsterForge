using TMPro;
using UnityEngine;

public class dummy_component : Health_Component
{
    public GameObject canvasObj;
    public GameObject textNum;
    public override void damageEntity(float damage)
    {
        currentHealth = Mathf.Max(currentHealth - damage, 0);
        Debug.Log($" Damaged {this.gameObject} for {damage}, current HP: {currentHealth}");

        
        if (currentHealth <= 0)
        {
            
            die();
        }
        //instantiate number.
        textNum.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        GameObject.Instantiate(textNum, canvasObj.transform);

    }


}