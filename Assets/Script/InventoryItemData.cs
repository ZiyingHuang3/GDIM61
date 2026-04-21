using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewInventoryItem", menuName = "Inventory/Item Data")]
public class InventoryItemData : ScriptableObject
{
    public string itemName;

    [TextArea(3, 6)]
    public string description;

    public Sprite icon;
}
