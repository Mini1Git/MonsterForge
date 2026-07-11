using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager Instance;

    public GameObject weaponContainer;

    private void Awake()
    {
        Instance = this;
        weaponContainer.SetActive(false);
    }

    public void showWeaponContainer(bool show)
    {
        if (show)
        {
            weaponContainer.SetActive(true);
        }
        else
        {
            weaponContainer.SetActive(false);
        }
    }
}
