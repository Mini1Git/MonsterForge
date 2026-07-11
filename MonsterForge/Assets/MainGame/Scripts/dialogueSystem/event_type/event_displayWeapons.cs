using UnityEngine;

[CreateAssetMenu(fileName = "event_displayWeapons", menuName = "Dialogue/Events/event_displayWeapons")]
public class Event_displayWeapons : Dialogue_event
{
    
    public override void executeEvent()
    {
        GameObjectManager.Instance.showWeaponContainer(true);

    }
}
