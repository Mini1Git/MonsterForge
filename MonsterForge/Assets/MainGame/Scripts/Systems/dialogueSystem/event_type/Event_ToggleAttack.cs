using UnityEngine;
[CreateAssetMenu(fileName = "ToggleAttack", menuName = "Dialogue/Events/ToggleAttack")]
public class Event_ToggleAttack : Dialogue_event
{
    public bool toggleAttack = false;
    public override void executeEvent(dialogueContext context)
    {
        if (toggleAttack)
        {
            Debug.Log("Enable attacking of weapons!");
            GameManager.Instance.player.GetComponent<PlayerAttack>().enableAttack();
        }
        else
        {
            Debug.Log("Disable attacking");
            GameManager.Instance.player.GetComponent<PlayerAttack>().disableAttack();
        }
    }
}
