using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    public static InventoryManager Instance;

    public List<InventoryItemData> items = new List<InventoryItemData>();
    private void Awake()
{
    if (Instance == null)
    {
        Instance = this;
        DontDestroyOnLoad(gameObject); // ⭐ 跨场景保留

        items.Clear(); // ⭐ 只在游戏第一次创建时清空
    }
    else
    {
        Destroy(gameObject);
    }
}

  public void AddItem(InventoryItemData item)
{
    if (item == null) return;

    if (!items.Contains(item))
    {
        items.Add(item);

        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.RefreshInventoryUI();
        }
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
