using UnityEngine;

public class npc_openDoor : MonoBehaviour
{
    [SerializeField] private Door_Component door;
    
    public void openDoor()
    {
        door.Open();
    }
}
