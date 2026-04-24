using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public List<InventoryItemData> items = new List<InventoryItemData>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void AddItem(InventoryItemData item)
    {
        if (item == null) return;

        items.Add(item);

        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.RefreshInventoryUI();
        }
    }
    public bool HasItem(InventoryItemData item)
{
    return items.Contains(item);
}

public bool HasAnyItem()
{
    return items.Count > 0;
}
}
