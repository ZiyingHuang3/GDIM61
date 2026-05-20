using UnityEngine;

public class Part2EvidenceChecker : MonoBehaviour
{
    public InventoryItemData Button;
    public InventoryItemData Camera;
    public InventoryItemData Video;
    public InventoryItemData Notebook;

    private void Update()
    {
        if (InventoryManager.Instance == null) return;

        if (!GameProgress.part2EvidenceComplete &&
            InventoryManager.Instance.HasItem(Camera) &&
            InventoryManager.Instance.HasItem(Button) &&
            InventoryManager.Instance.HasItem(Notebook) &&
            InventoryManager.Instance.HasItem(Video))
        {
            GameProgress.part2EvidenceComplete = true;
            Debug.Log("Part 2 evidence complete!");
          DialogueManager.Instance.StartSingleLineDialogue(
    "Cat",
    "The case is reaching its end, detective. When you're ready, follow the arrow and make your final deduction."
);  
        }
    }
}