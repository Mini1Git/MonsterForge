using System.Collections.Generic;
using UnityEngine;

public class DialogueStateManager : MonoBehaviour
{
    public static DialogueStateManager Instance { get; private set; } // read only.
    public Dictionary<string, NPC_DialogueState> npcDialogueState_list = new();
    
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }
    public void setDialogueNPC_State(NPCDialogue_Controller npc, DialogueState state)
    {
        npcDialogueState_list[npc.ID].state = state;
    }
}
