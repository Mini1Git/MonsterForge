using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
     
    public DialogueNode currentNode;
    public NPCDialogue_Controller current_Npc;
    public int indexDialogue = 0;
    public static DialogueManager Instance;
    public GameObject player;

    PlayerInput playerInput;
    public InputAction interact;
    bool doneOnCompleteEvents = false;

    private void Awake()
    {
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        
        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        interact = playerInput.Player.Interact; 
        interact.Enable();
        
    }
    private void OnDisable()
    {
        interact?.Disable();
        
    }
    public void setCurrentNPC(NPCDialogue_Controller npc)
    {
        current_Npc = npc;

        if (string.IsNullOrEmpty(npc.ID))
        {
            Debug.LogWarning("Need to add a ID to the NPC!");
            return;
        }
        //use gameManager here to keep track of npcs talked with
        if (!DialogueStateManager.Instance.npcDialogueState_list.ContainsKey(npc.ID))
        {
            Debug.Log("New NPC encountered!");
            currentNode = npc.startingNode;
            DialogueStateManager.Instance.npcDialogueState_list.Add(npc.ID, npc.NPC_State);
            
        }
        else
        {
            
            Debug.Log($"Already encountered {npc.ID}"); 
            if (npc.NPC_State.wasInterrupted)
            {
                
                currentNode = npc.interruptDialogueNode;
                indexDialogue = 0;
            }
            else
            {
                checkDialogueNode();
            }
        }
        
    }
    public void checkDialogueNode()
    {
        
        
        if (indexDialogue >= currentNode.dialogueText.Count && currentNode.nextNode != null && !DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].wasInterrupted)
        {

            Debug.Log("GOING TO NEXT NODE");
            currentNode = currentNode.nextNode;
            indexDialogue = 0;
            doneOnCompleteEvents = false;
            //basically reset everything, so reset the index, completeEvents bool, and set currentNode to the next one.
        }
        

    }
    private void Update()
    {
        
        
    }
    public void Subscribe() // subs to the function.
    {
        interact.performed += goNextDialogue;
    }
    public void Unsubscribe()
    {
        interact.performed -= goNextDialogue;
    }

    public void goNextDialogue(InputAction.CallbackContext context) {

            displayDialogue();
            
        if (DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.hasNotStarted 
            || DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.DoneTalking)
        {
            DialogueStateManager.Instance.setDialogueNPC_State(current_Npc, DialogueState.inProgress); // which means we already talked to them...
        }

        handleInterruption();
        
        
        if (handleDialogueCompletion())
        {
            return;
        }

    }
    public void handleInterruption()
    {
        //we want to check this before checking if we are done current node, as this is an interruption.
        if (DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].wasInterrupted
            && DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.inProgress)
        // if was interrupted in middle of the node's dialogue.
        {
            if (indexDialogue >= currentNode.dialogueText.Count)
            {
                
                DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].wasInterrupted = false;
                currentNode = DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].resumeNode;
                indexDialogue = 0;
                return;
            }
            

        }
    }
    public void OnCompleteEvents()
    {
        
        if (doneOnCompleteEvents)
            return;

        if (indexDialogue >= currentNode.dialogueText.Count)
        {
            Debug.Log("Doing on Complete events!");
            //executes only one time.
            dialogueContext dialogueContext = new dialogueContext(player, current_Npc); // gets context as to who is talking to who.

            for (int i = 0; i < currentNode.onComplete.Length; i++) // executes all the onComplete events.
            {
                currentNode.onComplete[i].executeEvent(dialogueContext);
            }


        }
    }
    public bool handleDialogueCompletion()
    {
        bool done = false;
        //we should check if done the current node.
        if (indexDialogue >= currentNode.dialogueText.Count && currentNode.nextNode != null)
        {
            Debug.Log("DONE CURRENT NODE!");
            DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state = DialogueState.DoneTalking;
            
        }
        if (currentNode.nextNode == null && indexDialogue >= currentNode.dialogueText.Count)
        { // if fully exhausted dialogue for npc.

            Debug.Log($"Done dialogue for {current_Npc.ID}");
            current_Npc.textGameObj.text = currentNode.dialogueText[currentNode.dialogueText.Count - 1]; // just repeat last line. for now.
            DialogueStateManager.Instance.setDialogueNPC_State(current_Npc, DialogueState.DoneTalking);

            done = true;
        }
        if (!doneOnCompleteEvents && indexDialogue >= currentNode.dialogueText.Count)
        { // if we haven't done the onComplete events yet, and we've reached the end of the dialogue.
            Debug.Log("Going to oncompleteEvent");
            OnCompleteEvents();
            doneOnCompleteEvents = true;
        }
        
        
        if (done)
        {
            return true;
        }
        else {  return false; }
    }
    public void displayDialogue()
    {
        if (indexDialogue < currentNode.dialogueText.Count)
        {
            current_Npc.textGameObj.text = currentNode.dialogueText[indexDialogue];
            indexDialogue++;
        }
        
    }
}
