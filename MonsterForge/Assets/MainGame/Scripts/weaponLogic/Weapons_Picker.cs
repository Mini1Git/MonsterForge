
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Picker : MonoBehaviour
{
    public Weapon_SO[] weapons_SO; // this will be fixed before runtime.
    //now need to instantiate these weapons as gameobjects
    public List<GameObject> weapons;

    public Weapon_SO weaponEquipped;
    public bool choseAWeapon;
    
    private void Start()
    {
        foreach (var weapon in weapons_SO)
        {
            
            GameObject tagged = GameObject.FindGameObjectWithTag(weapon.name + "Location"); // gets location of all of the weapons (provided they have tag)
            GameObject weaponGameObject = Instantiate(weapon.prefab,tagged.transform);
            weaponGameObject.GetComponent<SpriteRenderer>().sprite = weapon.icon;
            //parent


            weapons.Add(weaponGameObject);

            

        }
    }

    public void Update()
    {
        if (weaponEquipped != null && choseAWeapon)
        {
            choseWeapon();
            choseAWeapon = false; // so it only plays once.
            // now give the player a idle animation with the weapon.
        }
    }

    public void choseWeapon()
    {
        GameObjectManager.Instance.showWeaponContainer(false);
    }


}
