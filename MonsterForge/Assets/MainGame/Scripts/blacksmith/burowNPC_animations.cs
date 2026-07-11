using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class burowNPC_animations : MonoBehaviour
{
    

    Animator animator;
    public void Awake()
    {
    }
    public void Start()
    {
        animator = GetComponent<Animator>();
    }
 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("playerNear_Bool", true);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        animator.SetBool("playerNear_Bool", false);
    }
}
