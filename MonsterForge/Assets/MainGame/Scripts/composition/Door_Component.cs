using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class Door_Component : MonoBehaviour
{
    PlayerInput input;
    InputAction interact;
    public GameObject textPrompt;
    public string sceneName;
    bool isUnlocked;
    
    private void Awake()
    {
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        interact = input.Player.Interact;
        interact.Enable();
    }
    private void OnDisable()
    {
        interact.Disable();
    }
    public void Open()
    {
        Debug.Log("Door has been unlocked.");
        isUnlocked = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")){
            textPrompt.SetActive(true);
            interact.performed += Enter;
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            interact.performed -= Enter;
        }
    }

    public void Enter(InputAction.CallbackContext context)
    {
        if (isUnlocked)
        {
            Debug.Log($"Entering {sceneName}");
            SceneManager.LoadScene(sceneName);
            UIManager.Instance.findNewHealthBars();
            UIManager.Instance.updateHealthUI();
            
        }
        else
        {
            Debug.Log("The Door is Locked! Talk to Burow before heading out!");
        }
    }

}
