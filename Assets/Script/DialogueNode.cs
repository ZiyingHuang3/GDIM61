using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class DialogueNode
{
    public SpeakerType speaker;   
    public string speakerName;   

    [TextArea(2, 4)]
    public string dialogueText;

    public bool hasChoices;
    public List<DialogueChoice> choices = new List<DialogueChoice>();

    public int nextNodeIndex = -1;
    public int afterAllChoicesNextNode = -1;
}