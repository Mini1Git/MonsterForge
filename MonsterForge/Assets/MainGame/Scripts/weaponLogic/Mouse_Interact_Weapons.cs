using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse_Interact_Weapons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    public Weapon_SO weaponInfo;
    Weapons_Picker weaponPicker;
    Material outlineMat;
    private void Awake()
    {
        
    }
    private void Start()
    {
        weaponPicker = GameObjectManager.Instance.weaponContainer.GetComponent<Weapons_Picker>();
        

        outlineMat = gameObject.GetComponent<SpriteRenderer>().material;
        outlineMat.SetVector("_outlineThickness", new Vector2(0f,0f));

       
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        outlineMat.SetVector("_outlineThickness", new Vector2(0.02f, 0f));
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        outlineMat.SetVector("_outlineThickness", new Vector2(0f, 0f));

    }

    public void OnPointerClick(PointerEventData eventData)
    {
        outlineMat.SetColor("_outlineColour",Color.green);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        weaponPicker.weaponEquipped = weaponInfo;
        weaponPicker.choseAWeapon = true;
    }
}
