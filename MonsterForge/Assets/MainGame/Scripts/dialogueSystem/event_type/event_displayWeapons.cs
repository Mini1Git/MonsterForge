using UnityEngine;

[CreateAssetMenu(fileName = "Event_displayWeapons", menuName = "Dialogue/Events/displayWeapons")]
public class Event_displayWeapons : Dialogue_event
{
    
    public override void executeEvent(dialogueContext dialogueContext)
    {
        GameObjectManager.Instance.showWeaponContainer(true);

    }
}
