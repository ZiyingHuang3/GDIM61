using UnityEngine;
using UnityEngine.EventSystems;

public class BookcaseKeyPickup : MonoBehaviour
{
    public InventoryItemData keyItem;
    private bool picked = false;

    private void OnMouseDown()
    {
        Debug.Log("Clicked bookcase");

        if (picked) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            Debug.Log("Blocked by UI");
            return;
        }

        if (keyItem == null)
        {
            Debug.LogError("Key Item is missing!");
            return;
        }

        if (InventoryManager.Instance == null)
        {
            Debug.LogError("InventoryManager is missing!");
            return;
        }

        InventoryManager.Instance.AddItem(keyItem);
        picked = true;

        Debug.Log("Got key: " + keyItem.itemName);
    }
}