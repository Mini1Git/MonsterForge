using UnityEngine;
using UnityEngine.EventSystems;

public class Mouse_Interact_Weapons : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerUpHandler
{
    SpriteRenderer sr;
    Color og;
    Weapons_Picker weaponPicker;
    private void Start()
    {
        weaponPicker = GameObject.FindGameObjectWithTag("weaponContainer").GetComponent<Weapons_Picker>();
        sr = gameObject.GetComponent<SpriteRenderer>();
        og = sr.color;
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        sr.color = Color.yellow;
        
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        sr.color = og;
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        sr.color = Color.red;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        weaponPicker.chosenWeapon = gameObject.name;
        weaponPicker.choseAWeapon = true;
    }
}
