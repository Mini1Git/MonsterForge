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
    public bool doneOnCompleteEvents = false;
    public bool newScene = true;
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
        player = GameManager.Instance.player;
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
            //this is when it's a new scene while encountering same npc.
            newScene = false;
            currentNode = npc.startingNode;
            DialogueStateManager.Instance.npcDialogueState_list.Add(npc.ID, npc.NPC_State);
            
        }
        else if (GameManager.Instance.global_deathCounter > 0 && GameManager.Instance.respawned)
        {
            GameManager.Instance.respawned = false; // reset, so it should continue like normal.
            //then we assume a reset happened. or Respawn.
            DialogueStateManager.Instance.npcDialogueState_list.Clear();
            indexDialogue = 0;

            npc.NPC_State.state = DialogueState.hasNotStarted;
            newScene = false;
            currentNode = npc.startingNode;
            DialogueStateManager.Instance.npcDialogueState_list.Add(npc.ID, npc.NPC_State);
        }
        else
        {

            Debug.Log($"Already encountered {npc.ID}");
            Debug.LogWarning(current_Npc.ID);

            if (!DialogueStateManager.Instance.npcDialogueState_list[npc.ID].wasInterrupted && newScene)
            {
                //this is when it's a new scene while encountering same npc.

                newScene = false;
                currentNode = npc.startingNode;
                indexDialogue = 0; // reset everything dialogue related.
            }
            if (DialogueStateManager.Instance.npcDialogueState_list[npc.ID].wasInterrupted) // this checks if the npc got initially interrupted,
                                                                                            // and swaps the current node with the interrupt node
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
        
        
        if (indexDialogue >= currentNode.dialogueText.Count && currentNode.nextNode != null && 
            !DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].wasInterrupted)
        {

            Debug.Log("GOING TO NEXT NODE");
            currentNode = currentNode.nextNode;
            indexDialogue = 0;
            doneOnCompleteEvents = false;
            DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state = DialogueState.hasNotStarted;
            //basically reset everything, so reset the index, completeEvents bool, and set currentNode to the next one.
            // THIS HANDLES GOING TO NEXT DIALOGUE NODE.
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

        Debug.Log(DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state.ToString());
        if (DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.hasNotStarted)
        {
            Debug.LogWarning("HAS NOT STARTED => IN PROGRESS");
            DialogueStateManager.Instance.setDialogueNPC_State(current_Npc, DialogueState.inProgress); // which means we have not talked to them... in the new node.
            doneOnCompleteEvents = false;
            
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

            doneOnCompleteEvents = true;

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
        if (indexDialogue >= currentNode.dialogueText.Count)
        {
            Debug.Log("DONE CURRENT NODE!");
            DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state = DialogueState.DoneTalking;
            
        }
        if (currentNode.nextNode == null && DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.DoneTalking)
        { // if fully exhausted dialogue for npc. After we've done the onCompleteEvents.

            Debug.Log($"Done dialogue for {current_Npc.ID}");
            current_Npc.textGameObj.text = currentNode.dialogueText[currentNode.dialogueText.Count - 1]; // just repeat last line. for now.
            DialogueStateManager.Instance.setDialogueNPC_State(current_Npc, DialogueState.DoneTalking);

            done = true;
        }
        if (!doneOnCompleteEvents && DialogueStateManager.Instance.npcDialogueState_list[current_Npc.ID].state == DialogueState.DoneTalking)
        { // if we haven't done the onComplete events yet, and we've reached the end of the dialogue.
            Debug.LogWarning("Going to oncompleteEvent");
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
