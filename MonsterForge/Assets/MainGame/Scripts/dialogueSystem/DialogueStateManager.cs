using System.Collections.Generic;
using UnityEngine;
public enum DialogueState
{
    hasNotStarted,
    inProgress,
    WaitingOnQuest,
    DoneTalking
}
public class DialogueStateManager : MonoBehaviour
{
    public static DialogueStateManager Instance { get; private set; } // read only.
    public Dictionary<string, NPC_DialogueState> npcDialogueState_list = new();
    
    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }
    public void setDialogueNPC_State(NPCDialogue_Controller npc, DialogueState state)
    {
        npcDialogueState_list[npc.ID].state = state;
    }
}
