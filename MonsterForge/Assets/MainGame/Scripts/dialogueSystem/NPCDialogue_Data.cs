using TMPro;
using UnityEngine;

public class NPCDialogue_Data : MonoBehaviour
{
    public TextMeshProUGUI textGameObj;
    public DialogueManager dm;
    public DialogueNode startingNode;
    
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HEY YOU ENTERED TALKING!");
        dm.current_Npc = this;
        dm.startDialogue(this);
        textGameObj.text = "E";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dm.current_Npc = null;
        textGameObj.text = null;
    }
    private void Awake()
    {
        
        textGameObj = GetComponentInChildren<TextMeshProUGUI>();
        textGameObj.text = null;
    }
}
