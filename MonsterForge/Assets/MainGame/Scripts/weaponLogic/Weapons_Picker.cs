
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Picker : MonoBehaviour
{
    public Weapon_SO[] weapons_SO; // this will be fixed before runtime.
    //now need to instantiate these weapons as gameobjects
    public List<GameObject> weapons;

    public string chosenWeapon = null;
    public Weapon_SO weaponEquipped;
    public bool choseAWeapon;
    
    private void Start()
    {
        foreach (var weapon in weapons_SO)
        {
            GameObject weaponGameObject = Instantiate(weapon.prefab);
            weaponGameObject.GetComponent<SpriteRenderer>().sprite = weapon.icon;
            GameObject tagged = GameObject.FindGameObjectWithTag(weapon.name + "Location"); // gets location of all of the weapons (provided they have tag)
            weaponGameObject.transform.position = tagged.transform.position;
            //parent
     

            weapons.Add(weaponGameObject);

            

        }
    }

    public void Update()
    {
        if (chosenWeapon != null && choseAWeapon)
        {
            StartCoroutine(choseWeapon());
            Debug.Log("You chose the " + chosenWeapon);
            choseAWeapon = false; // so it only plays once.
            // now give the player a idle animation with the weapon.
        }
    }

    IEnumerator choseWeapon()
    {
        if (weapons.Count > 1)
        {
            foreach (var weapon in weapons)
            {
                if (weapon.name == chosenWeapon)
                {
                    weaponEquipped = weapon.GetComponent<Mouse_Interact_Weapons>().weaponInfo; // weaponEquipped is weaponSO
                    continue;
                }
                weapon.SetActive(false);
                yield return null;
            }
        }
        else
        {
            Debug.Log("only one weapon!");
            yield return null;
        }
    }


}
