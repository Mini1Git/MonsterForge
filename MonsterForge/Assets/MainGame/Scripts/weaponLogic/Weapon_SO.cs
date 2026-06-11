using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class Weapon_SO : ScriptableObject
{
    
    string weaponName;
    public GameObject prefab;
    public string attackType;
    public int damage;
    public Sprite icon;
    public AnimationClip idleAnimation;
    public AnimationClip walkAnimation;
    public AnimationClip attackAnimation;
    
    public void Awake()
    {
        
        weaponName = name;
    }

}

