using UnityEngine;
using UnityEngine.EventSystems;

public class BodyEvidencePickup : MonoBehaviour
{
    public InventoryItemData autopsyReport;
    public GameObject collectHint;

    private bool collected = false;

    private void Start()
    {
        if (collectHint != null)
            collectHint.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (collected) return;

        if (InventoryUIManager.Instance != null &&
            InventoryUIManager.Instance.inventoryPanel.activeSelf)
            return;

        if (InventoryManager.Instance == null) return;
        if (autopsyReport == null) return;

        InventoryManager.Instance.AddItem(autopsyReport);

        collected = true;
        if (collectHint != null)
        {
            collectHint.SetActive(true);
            Invoke(nameof(HideCollectHint), 2f);
        }

    }
    void HideCollectHint()
    {
        if (collectHint != null)
            collectHint.SetActive(false);
    }
}