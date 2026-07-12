using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class Weapon_SO : ScriptableObject
{
    
    string weaponName;
    public GameObject prefab;
    public int damage;
    public Sprite icon;
    public AnimatorOverrideController animatorController;

    public GameObject projectile;
    public enum AttackType{
        Melee,
        ProjectileBased
    }

    public AttackType attackType;
    public void Awake()
    {
        weaponName = name;
    }

}

