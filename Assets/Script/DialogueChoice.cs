
using System;

[Serializable]
public class DialogueChoice
{
    public string choiceText;
    public int nextNodeIndex;
    public InventoryItemData requiredEvidence;
}