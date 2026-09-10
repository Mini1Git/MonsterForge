using System.Collections;
using TMPro;
using UnityEngine;

public class creditsFloat : MonoBehaviour
{
    public TextMeshProUGUI text;
    Coroutine textCoroutine = null;
    public int count; 
    public string oldString = "";
    public string diffCreditString = "";
    public string newString = "";

    public void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    public void Start()
    {
        
        oldString = text.text;
    }
    private void Update()
    {
        if (textCoroutine == null)
        {
            Debug.Log(count);
            textCoroutine = StartCoroutine(floatText());
        }

    }
    private IEnumerator floatText()
    {
        count++;
        yield return new WaitForSeconds(0.5f);
        newString += "\n";
        text.text = newString + oldString;

        if (count >= 10 && count <= 18)
        {
            diffCreditString += "Audio made by Mini1";
            text.text = diffCreditString + newString + oldString;

        }
        if (count > 18 && count <= 24)
        {
            diffCreditString += "Art made by Mini1";
            text.text = diffCreditString + newString + oldString;
        }
        if (count > 24 && count <= 30)
        {
            diffCreditString += "Programming by Mini1";
            text.text = diffCreditString + newString + oldString;
        }
        if (count > 30 && count <= 305)
        {
            diffCreditString += "MONSTER\nFORGE";
            text.text = diffCreditString + newString + oldString;
        }
        //resets
        if (count == 8 || count == 18 || count == 24 || count == 30)
        {
            diffCreditString = "";
        }
        diffCreditString += "\n";
        textCoroutine = null;
    }
}
