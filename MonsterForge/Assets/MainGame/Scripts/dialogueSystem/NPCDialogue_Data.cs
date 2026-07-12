using TMPro;
using UnityEngine;

public class NPCDialogue_Data : MonoBehaviour
{
    public TextMeshProUGUI textGameObj;
    public DialogueNode startingNode;
    
    [SerializeField] private DialogueManager dm;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("HEY YOU ENTERED TALKING!");
        dm.current_Npc = this; // this stores the npc the player is talking to.
        dm.startDialogue(this);
        textGameObj.text = "E";
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        dm.current_Npc = null;
        textGameObj.text = null;
    }
    public void Start()
    {
        dm = DialogueManager.Instance;
    }
    private void Awake()
    {
        
        textGameObj = GetComponentInChildren<TextMeshProUGUI>();
        textGameObj.text = null;
    }
}
