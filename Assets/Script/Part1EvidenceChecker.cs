using UnityEngine;

public class Part1EvidenceChecker : MonoBehaviour
{
    public InventoryItemData coffee;
    public InventoryItemData pen;
    public InventoryItemData autopsy;
    public InventoryItemData calendar;
    public InventoryItemData checkin;

    private void Update()
    {
        if (InventoryManager.Instance == null) return;

        if (!GameProgress.part1EvidenceComplete &&
            InventoryManager.Instance.HasItem(coffee) &&
            InventoryManager.Instance.HasItem(pen) &&
               InventoryManager.Instance.HasItem(calendar) &&
               InventoryManager.Instance.HasItem(checkin) &&
            InventoryManager.Instance.HasItem(autopsy))
        {
            GameProgress.part1EvidenceComplete = true;
            Debug.Log("Part 1 evidence complete!");
        }
    }
}