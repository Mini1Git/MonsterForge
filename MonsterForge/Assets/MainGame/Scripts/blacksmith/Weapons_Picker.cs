
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Picker : MonoBehaviour
{
    public Weapon_SO[] weapons_SO; // this will be fixed before runtime.
    //now need to instantiate these weapons as gameobjects
    public List<GameObject> weapons;

    public string chosenWeapon = null;
    public bool choseAWeapon;
    private void Start()
    {
        foreach (var weapon in weapons_SO)
        {
            GameObject weaponGameObject = new GameObject(weapon.name);
            weaponGameObject.AddComponent<SpriteRenderer>().sprite = weapon.icon;
            weaponGameObject.transform.position = GameObject.FindGameObjectWithTag(weapon.name + "Location").transform.position;
            //parent
            weaponGameObject.transform.SetParent(GameObject.FindGameObjectWithTag(weapon.name + "Location").transform);

            weaponGameObject.AddComponent<BoxCollider2D>().isTrigger = true;
            weaponGameObject.GetComponent<BoxCollider2D>().size = new Vector2(1.2f, 1.2f);
            weaponGameObject.AddComponent<Mouse_Interact_Weapons>();
            

            weapons.Add(weaponGameObject);

            

        }
    }

    public void Update()
    {
        if (chosenWeapon != null && choseAWeapon)
        {
            StartCoroutine(choseWeapon());
            Debug.Log("You chose the " + chosenWeapon);
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
