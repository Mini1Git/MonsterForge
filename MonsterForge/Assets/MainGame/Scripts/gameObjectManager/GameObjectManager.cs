using UnityEngine;

public class GameObjectManager : MonoBehaviour
{
    public static GameObjectManager Instance;

    public GameObject weaponContainer;
    
    public Weapon_SO currentWeapon;
    bool showedOnce = false;
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        if (weaponContainer != null)
        {
            weaponContainer.SetActive(false);
        }
    }
    public void Start()
    {
        
    }

    public void showWeaponContainer(bool show)
    {
        if (show && !showedOnce)
        {
            
            weaponContainer.SetActive(true);
        }
        else
        {
            showedOnce = true;
            currentWeapon = weaponContainer.GetComponent<Weapons_Picker>().weaponEquipped; //this means the player def picked a weapon.
            weaponContainer.SetActive(false);
        }
    }
}
