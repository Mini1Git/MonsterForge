using UnityEngine;

public class dialogueContext
{
    public GameObject Player;
    public NPCDialogue_Data NPC;
    public dialogueContext(GameObject player, NPCDialogue_Data npc)
    {
        Player = player;
        NPC = npc;
    }
}
