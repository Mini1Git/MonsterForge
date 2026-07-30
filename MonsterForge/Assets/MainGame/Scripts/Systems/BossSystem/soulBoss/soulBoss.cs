using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class soulBoss : MonoBehaviour
{
    public string sceneName;
    public GameObject textPrompt;
    PlayerInput input;
    InputAction interact;
    
    private void Awake()
    {
        input = new PlayerInput();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textPrompt.SetActive(true);
            interact.performed += goBackToCamp;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            textPrompt.SetActive(false);
            interact.performed -= goBackToCamp;
        }
    }
    public void goBackToCamp(InputAction.CallbackContext context)
    {
        loadScene(sceneName);
    }
    public void loadScene(string scene)
    {
        SceneManager.LoadScene(scene);
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
}
