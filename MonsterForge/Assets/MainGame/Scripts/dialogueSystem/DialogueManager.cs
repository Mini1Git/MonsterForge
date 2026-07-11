using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
     
    public DialogueNode currentNode;
    public NPCDialogue_Data current_Npc;
    public int indexDialogue = 0;

    PlayerInput playerInput;
    InputAction interact;
    DialogueManager Instance;

    private void Awake()
    {
        playerInput = new PlayerInput();

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        Instance = this;

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

        currentNode = npc.startingNode;
        
    }
    private void Update()
    {
        if (currentNode != null)
        {
            interact.performed += goNextDialogue; // basically if player hits "e"
        }
    }

    public void goNextDialogue(InputAction.CallbackContext context) { 

        if (currentNode.nextNode == null && indexDialogue >= currentNode.dialogueText.Length)
        {
            Debug.Log("END OF SCRIPT");
            current_Npc.textGameObj.text = currentNode.dialogueText[currentNode.dialogueText.Length-1];

            currentNode.onComplete[0].executeEvent();

            return;
        }

        if (indexDialogue >= currentNode.dialogueText.Length)
        {
            currentNode = currentNode.nextNode;
            indexDialogue = 0;
        }
        displayDialogue();
        
    }

    public void displayDialogue()
    {
        
        current_Npc.textGameObj.text = currentNode.dialogueText[indexDialogue];
        indexDialogue++;
    }
}
