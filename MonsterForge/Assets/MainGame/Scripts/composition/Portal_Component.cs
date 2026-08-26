using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Portal_Component : MonoBehaviour
{
    PlayerInput input;
    InputAction interact;
    public GameObject textPrompt;
    public string sceneName;
    public Animator animator;
    bool isUnlocked = false;
    
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        interact = input.Player.Interact;
        interact.Enable();
        textPrompt.SetActive(false);
    }
    private void OnDisable()
    {
        interact.Disable();
    }
    public void Open()
    {
        Debug.Log("Portal has been unlocked.");
        isUnlocked = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            if (isUnlocked)
            {
                textPrompt.SetActive(true);
                animator.SetBool("playerNear", true);
            }
            
            interact.performed += Enter; // if click "e"
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            if (isUnlocked)
            {
                animator.SetBool("playerNear", false);
            }
            textPrompt.SetActive(false);
            interact.performed -= Enter;

        }
    }

    public void Enter(InputAction.CallbackContext context)
    {
        if (isUnlocked)
        {

            Debug.Log($"Entering {sceneName}");
            SceneManager.LoadScene(sceneName);
            
        }
        else
        {
            Debug.Log("The Door is Locked! Talk to Burow before heading out!");
        }
    }

}
