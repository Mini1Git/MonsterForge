using UnityEngine;

public class ability_openDoors : MonoBehaviour
{
   
    public Door_Component door;

    public void OpenDoor()
    {
        door.Open();
    }
}
