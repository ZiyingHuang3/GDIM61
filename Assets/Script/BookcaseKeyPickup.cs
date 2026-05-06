using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class BookcaseKeyPickup : MonoBehaviour
{
    public InventoryItemData keyItem;

    public GameObject keyGetText; 

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

        StartCoroutine(ShowKeyText());
        var sr = GetComponent<SpriteRenderer>();
        if (sr != null) sr.enabled = false;

        var col = GetComponent<Collider2D>();
        if (col != null) col.enabled = false;
    }

    IEnumerator ShowKeyText()
    {
        keyGetText.SetActive(true);

        yield return new WaitForSeconds(2f);

        keyGetText.SetActive(false);
    }
}