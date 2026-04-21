using UnityEngine;

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

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            PickUp();
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
