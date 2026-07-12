using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogueNode", menuName = "Dialogue/Normal Node")]
public class DialogueNode : ScriptableObject
{
    public string character;
    public List<string> dialogueText;
    public DialogueNode nextNode;
    

    public Dialogue_event[] onComplete;
}
