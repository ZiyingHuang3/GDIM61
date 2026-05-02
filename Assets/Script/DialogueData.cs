using System.Collections.Generic;
using UnityEngine;

public class DialogueData : MonoBehaviour
{
    public bool isIntroDialogue = false;
    [Header("Progress On Dialogue End")]
    public bool markSoulDialogueCompleteOnEnd = false;
    public List<DialogueNode> nodes = new List<DialogueNode>();
}