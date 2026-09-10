using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Event_ENDGAME", menuName = "Dialogue/Events/End Game")]
public class Event_ENDGAME : Dialogue_event
{

    public override void executeEvent(dialogueContext context)
    {
        GameManager.Instance.loadCredits();
    }
    
    
}
