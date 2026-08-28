using UnityEngine;

[CreateAssetMenu(fileName = "Event_displayWeapons", menuName = "Dialogue/Events/displayWeapons")]
public class Event_displayWeapons : Dialogue_event
{
    
    public override void executeEvent(dialogueContext dialogueContext)
    {
        Debug.LogWarning("SHOW WEAPON CONTAINER");
        GameObjectManager.Instance.showWeaponContainer(true);

    }
}
