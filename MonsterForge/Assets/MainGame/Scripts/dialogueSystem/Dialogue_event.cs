using System;
using UnityEngine;

public abstract class Dialogue_event: ScriptableObject
{
    public abstract void executeEvent(dialogueContext context);
}
