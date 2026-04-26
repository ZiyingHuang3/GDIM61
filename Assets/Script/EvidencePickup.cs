using UnityEngine;
using UnityEngine.EventSystems;
public class EvidencePickup : MonoBehaviour
{
    public InventoryItemData itemData;
    public GameObject pickupHint;

    private bool playerInRange = false;

    private void Start()
    {
        if (pickupHint != null)
            pickupHint.SetActive(false);
    }

  private bool picked = false;

private void Update()
{
    if (picked) return;
    if (!playerInRange) return;

    if (Input.GetMouseButtonDown(0))
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (InventoryUIManager.Instance != null &&
            InventoryUIManager.Instance.inventoryPanel.activeSelf)
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject == gameObject)
            {
                picked = true;
                PickUp();
                return;
            }
        }
    }
}

    private void PickUp()
    {
        if (itemData == null) return;

        InventoryManager.Instance.AddItem(itemData);

        if (pickupHint != null)
            pickupHint.SetActive(false);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;

            if (pickupHint != null)
                pickupHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;

            if (pickupHint != null)
                pickupHint.SetActive(false);
        }
    }
}