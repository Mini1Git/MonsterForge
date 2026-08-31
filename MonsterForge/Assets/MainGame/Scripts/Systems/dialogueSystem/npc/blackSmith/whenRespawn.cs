using UnityEngine;

public class whenRespawn : MonoBehaviour
{
    public DialogueNode[] dialogueNodes;
    public DialogueNode endNode;
    public int deathAmount = 0;

    private void Start()
    {
        deathAmount = GameManager.Instance.global_deathCounter;
        if (deathAmount <= dialogueNodes.Length && deathAmount > 0)
        {
            //get the blackSmith's starting node info.
            Debug.LogWarning("MODIFIED?");
            GameObject.FindGameObjectWithTag("Burow").GetComponent<NPCDialogue_Controller>().startingNode = dialogueNodes[deathAmount-1];
            
        }
        else if (deathAmount > dialogueNodes.Length)
        {
            GameObject.FindGameObjectWithTag("Burow").GetComponent<NPCDialogue_Controller>().startingNode = endNode;
        }
            
    }
}
