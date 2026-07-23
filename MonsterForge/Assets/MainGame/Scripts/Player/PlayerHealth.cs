using UnityEngine;

public class PlayerHealth : Health_Component
{
    public override void damageEntity(float damage)
    {
        Debug.Log($"Damaged the player for {damage}");
        base.damageEntity(damage);
        // UI updates
        UIManager.Instance.updateHealthUI();
    }
}
