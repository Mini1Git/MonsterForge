using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Event_OpenPortal", menuName = "Dialogue/Events/Portal")]
public class Event_OpenPortal : Dialogue_event
{
    
    public override void executeEvent(dialogueContext dialogueContext)
    {
        ability_openPortal doorOpener = dialogueContext.NPC.GetComponent<ability_openPortal>();
       
        if (doorOpener != null)
        {
            
            doorOpener.OpenPortal();
        }
        
    }
}
