using TMPro;
using UnityEngine;

public class NPCDialogue_Controller : MonoBehaviour
{

    public string ID;
    public NPC_DialogueState NPC_State;
    public TextMeshProUGUI textGameObj;
    public DialogueNode startingNode;
    
    public DialogueNode interruptDialogueNode;
    [SerializeField]
    private DialogueManager dm;

    public virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            dm.setCurrentNPC(this);
            textGameObj.text = "E";
            dm.Subscribe();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }
        dm.current_Npc = null;
        textGameObj.text = null;
        if (DialogueStateManager.Instance.npcDialogueState_list[ID].state == DialogueState.inProgress)
        {
            DialogueStateManager.Instance.npcDialogueState_list[ID].wasInterrupted = true;
            DialogueStateManager.Instance.npcDialogueState_list[ID].resumeNode = dm.currentNode;
        }

        dm.Unsubscribe();
    }
    private void Start()
    {

        dm = DialogueManager.Instance;
    }
    private void Awake()
    {
        textGameObj = GetComponentInChildren<TextMeshProUGUI>();
        textGameObj.text = null;
    }
}
