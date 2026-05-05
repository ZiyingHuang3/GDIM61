using UnityEngine;
using UnityEngine.EventSystems;

public class LockedDrawer : MonoBehaviour
{
    public InventoryItemData requiredKey;

    public InventoryItemData rewardItem1;
    public InventoryItemData rewardItem2;

    public string lockedSpeakerName = "Hazel";

    [TextArea(2, 4)]
    public string lockedMessage = "It seems locked. I need to find a key first.";

    private bool opened = false;

    private void OnMouseDown()
    {
        if (opened) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (requiredKey == null || !InventoryManager.Instance.HasItem(requiredKey))
        {
            DialogueManager.Instance.StartSingleLineDialogue(
                lockedSpeakerName,
                lockedMessage
            );
            return;
        }

        OpenDrawer();
    }

    private void OpenDrawer()
    {
        opened = true;

        if (rewardItem1 != null)
            InventoryManager.Instance.AddItem(rewardItem1);

        if (rewardItem2 != null)
            InventoryManager.Instance.AddItem(rewardItem2);

        Debug.Log("Drawer opened.");
    }
}