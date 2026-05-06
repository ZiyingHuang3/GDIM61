using UnityEngine;
using UnityEngine.EventSystems;

public class BookcasesKeyPickup : MonoBehaviour
{
    [Header("Reward")]
    public InventoryItemData keyItem;

    [Header("UI")]
    public GameObject bookChoicePanel;

    [Header("Messages")]
    public string speakerName = "Hazel";

    [TextArea(2, 4)]
    public string firstPhaseMessage = "It feels like something is missing. I need more information first.";

    [TextArea(2, 4)]
    public string wrongBookMessage = "This doesn't seem right.";

    [TextArea(2, 4)]
    public string correctBookMessage = "This book moved... there is a key hidden behind it.";

    private bool gotKey = false;

    private void Start()
    {
        if (bookChoicePanel != null)
            bookChoicePanel.SetActive(false);
    }

    private void OnMouseDown()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        if (gotKey) return;

        // 第一次搜证阶段：还没返回 Map1
        if (!GameProgress.returnedToMap1)
        {
            DialogueManager.Instance.StartSingleLineDialogue(speakerName, firstPhaseMessage);
            return;
        }

        // 第二次搜证阶段：可以打开选书 UI
        if (bookChoicePanel != null)
            bookChoicePanel.SetActive(true);
    }

    public void ChooseCorrectBook()
    {
        if (gotKey) return;

        if (keyItem == null)
        {
            Debug.LogError("Key Item is missing on BookcasePuzzle.");
            return;
        }

        InventoryManager.Instance.AddItem(keyItem);
        gotKey = true;

        if (bookChoicePanel != null)
            bookChoicePanel.SetActive(false);

        DialogueManager.Instance.StartSingleLineDialogue(speakerName, correctBookMessage);
    }

    public void ChooseWrongBook()
    {
        DialogueManager.Instance.StartSingleLineDialogue(speakerName, wrongBookMessage);
    }

    public void CloseBookPanel()
    {
        if (bookChoicePanel != null)
            bookChoicePanel.SetActive(false);
    }
}