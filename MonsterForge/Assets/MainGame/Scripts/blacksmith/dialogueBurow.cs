using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class dialogueBurow : MonoBehaviour
{
    public string[] dialogues;

    GameObject weaponContainer; // to choose starter weapons.
    public TextMeshProUGUI textSource_Dialogue;
    public TextMeshProUGUI textSource_InteractPrompt;
    public string prompt;
    

    int i = 1;
    PlayerInput playerInput;
    InputAction interact;
    bool startedDialogue = false;
    bool reachedEndOfDialogue = false;
    public void Awake()
    {
        playerInput = new PlayerInput(); //needs to be on awake because OnEnable activates as soon as the object becomes active and start() is too late.
    }
    public void Start()
    {
       //deactivatese the weapon container, because we only activate it when Burow shows weapons.
       weaponContainer = GameObject.FindGameObjectWithTag("weaponContainer");
       weaponContainer.SetActive(false);

        textSource_Dialogue.text = null;
        textSource_InteractPrompt.text = null;
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
    public void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textSource_InteractPrompt.text = prompt;
        interact.performed += startDialogue; // basically if player hits "e"
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        textSource_InteractPrompt.text = null;
    }
    public void startDialogue(InputAction.CallbackContext context)
    {
        if (!startedDialogue)
        {
            textSource_Dialogue.text = dialogues[0];
            startedDialogue = true;
        }
        else if (!reachedEndOfDialogue)
        {
            goNextDialogue();
        }
        else
        {
            //maybe smt else?
        }
    }

    public void goNextDialogue()
    {
        int dialogueMaxSize = dialogues.Length;
        
        if (i >= dialogueMaxSize) // basically, if reach the end of dialogue, then burow will display weapons.
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            Debug.Log("DISPLAYING WEAPONS");
            displayWeapons();
            textSource_Dialogue.text = "Choose a weapon...";
            
            reachedEndOfDialogue = true;
            return;
        }
        textSource_Dialogue.text = dialogues[i++];
    }

    public void displayWeapons()
    {
        
        weaponContainer.SetActive(true); // show weapons
    }
}
