using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager Instance;

    public GameObject weaponContainer;
    public GameObject door;
    public Weapon_SO currentWeapon;
    GameObject player;
    bool showedOnce = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
        weaponContainer.SetActive(false);
    }
    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void showWeaponContainer(bool show)
    {
        if (show && !showedOnce)
        {
            showedOnce = true;
            weaponContainer.SetActive(true);
        }
        else
        {
            currentWeapon = weaponContainer.GetComponent<Weapons_Picker>().weaponEquipped; //this means the player def picked a weapon.
            player.GetComponent<PlayerAttack>().EquipWeapon(currentWeapon);
            weaponContainer.SetActive(false);
        }
    }
}
