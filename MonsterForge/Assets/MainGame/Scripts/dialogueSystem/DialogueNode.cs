using System;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Normal Node")]
public class DialogueNode : ScriptableObject
{
    public string character;
    public string[] dialogueText;
    public DialogueNode nextNode;
    // whats the purpose of using nextNode? as a state?

    public Dialogue_event[] onComplete;
}
