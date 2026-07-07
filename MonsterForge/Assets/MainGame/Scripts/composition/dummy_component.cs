using TMPro;
using UnityEngine;

public class dummy_component : Health_Component
{
    public GameObject canvasObj;
    public GameObject textNum;
    public override void damageEntity(float damage)
    {
        base.damageEntity(damage);
        //instantiate number.
        textNum.GetComponent<TextMeshProUGUI>().text = damage.ToString();
        GameObject.Instantiate(textNum, canvasObj.transform);

    }


}