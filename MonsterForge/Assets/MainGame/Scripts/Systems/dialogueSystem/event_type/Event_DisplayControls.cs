using JetBrains.Annotations;
using TMPro;
using UnityEngine;

[CreateAssetMenu(fileName = "Event_DisplayControls", menuName = "Dialogue/Events/Event_DisplayControls")]
public class Event_DisplayControls : Dialogue_event
{
    public string displayControls;

    public override void executeEvent(dialogueContext context)
    {
        TextMeshProUGUI text = GameObject.FindGameObjectWithTag("controlsText").GetComponent<TextMeshProUGUI>();
        text.text = displayControls;
    }
}
