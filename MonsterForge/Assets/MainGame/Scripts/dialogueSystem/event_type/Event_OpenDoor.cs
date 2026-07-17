using Unity.VisualScripting;
using UnityEngine;
[CreateAssetMenu(fileName = "Event_OpenDoor", menuName = "Dialogue/Events/open Door")]
public class Event_OpenDoor : Dialogue_event
{
    
    public override void executeEvent(dialogueContext dialogueContext)
    {
        ability_openDoors doorOpener = dialogueContext.NPC.GetComponent<ability_openDoors>();
       
        if (doorOpener != null)
        {
            Debug.Log("OPEN DOOR BRUH");
            doorOpener.OpenDoor();
        }
        
    }
}
