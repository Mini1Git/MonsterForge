using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
     
    public DialogueNode currentNode;
    public NPCDialogue_Data current_Npc;
    public int indexDialogue = 0;
    public static DialogueManager Instance;
    public GameObject player;

    PlayerInput playerInput;
    InputAction interact;
    bool startedDialogue = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        Instance = this;
        playerInput = new PlayerInput();
    }
    private void OnEnable()
    {
        interact = playerInput.Player.Interact; 
        interact.Enable();

    }
    private void OnDisable()
    {
        interact.Disable();
    }
    public void startDialogue(NPCDialogue_Data npc)
    {
        
        if (startedDialogue == false) {
            startedDialogue = true;
            currentNode = npc.startingNode;
        }
        else if (indexDialogue >= currentNode.dialogueText.Length && currentNode.nextNode != null)
        {
            Debug.Log("GOING TO NEXT NODE");
            currentNode = currentNode.nextNode;
            indexDialogue = 0;
        }
        else
        {
            Debug.Log("END OF SCRIPT");
        }

    }
    private void Update()
    {
        if (current_Npc != null)
        {
            interact.performed += goNextDialogue; // basically if player hits "e"
        }
        else
        {
            interact.performed -= goNextDialogue;
        }
        
    }

    public void goNextDialogue(InputAction.CallbackContext context) { 

        if (currentNode.nextNode == null && indexDialogue >= currentNode.dialogueText.Length)
        {
            Debug.Log("END OF SCRIPT");
            current_Npc.textGameObj.text = currentNode.dialogueText[currentNode.dialogueText.Length-1];
            

            return;
        }
        if (indexDialogue >= currentNode.dialogueText.Length) {//if we hit the max length of the dialogue of current node.
            return;
        }
        if (indexDialogue >= currentNode.dialogueText.Length - 1) {

            dialogueContext dialogueContext = new dialogueContext(player, current_Npc);
            currentNode.onComplete[0].executeEvent(dialogueContext);
        }

        displayDialogue();
        
    }

    public void displayDialogue()
    {
        
        current_Npc.textGameObj.text = currentNode.dialogueText[indexDialogue];
        indexDialogue++;
    }
}
