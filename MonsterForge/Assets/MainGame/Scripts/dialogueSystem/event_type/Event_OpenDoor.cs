using UnityEngine;
[CreateAssetMenu(fileName = "Event_OpenDoor", menuName = "Dialogue/Events/open Door")]
public class Event_OpenDoor : Dialogue_event
{
    public override void executeEvent(dialogueContext dialogueContext)
    {
        npc_openDoor doorOpener = dialogueContext.NPC.GetComponent<npc_openDoor>();
        if (doorOpener != null)
        {
            doorOpener.openDoor();
        }
        // this openDoor is a method that opens door from npc_openDoor.
        // check it out 
    }
}
