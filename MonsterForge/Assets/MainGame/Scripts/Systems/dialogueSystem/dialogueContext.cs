using UnityEngine;

public class dialogueContext
{
    public GameObject Player;
    public NPCDialogue_Controller NPC;
    public dialogueContext(GameObject player, NPCDialogue_Controller npc)
    {
        Player = player;
        NPC = npc;
    }
}
