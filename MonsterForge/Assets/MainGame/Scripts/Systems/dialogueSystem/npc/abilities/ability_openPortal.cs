using UnityEngine;

public class ability_openPortal : MonoBehaviour
{
   
    public Portal_Component portal;

    public void OpenPortal()
    {
        portal.Open();
    }
}
