using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Door_Component : MonoBehaviour
{
    PlayerInput input;
    InputAction interact;
    public GameObject textPrompt;

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
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        textPrompt.SetActive(true);
        interact.performed += Enter;
    }

    public void Enter(InputAction.CallbackContext context)
    {
        Debug.Log("Enter Outside");
        SceneManager.LoadScene("OutsideTest");
    }

}
