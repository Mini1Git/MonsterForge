using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Scriptable Objects/Weapons")]
public class Weapon : ScriptableObject
{
    
    string weaponName;
    public string attackType;
    public int damage;
    public Sprite icon;

    public AnimationClip attackAnimation;

    public void Awake()
    {
        weaponName = name;
    }

}

