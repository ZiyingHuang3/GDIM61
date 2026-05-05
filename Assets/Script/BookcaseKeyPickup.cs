using UnityEngine;
using UnityEngine.EventSystems;

public class BookcaseKeyPickup : MonoBehaviour
{
    public InventoryItemData keyItem;

    private bool picked = false;

    private void OnMouseDown()
    {
        if (picked) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (keyItem == null) return;

        InventoryManager.Instance.AddItem(keyItem);
        picked = true;

        Debug.Log("Got key: " + keyItem.itemName);
    }
}