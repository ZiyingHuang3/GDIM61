
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
    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        foreach (Collider2D hit in hits)
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
            {
                TryOpenBookcase();
                return;
            }
        }
    }
    private void TryOpenBookcase()
    {
        if (gotKey) return;

        if (!GameProgress.returnedToMap1)
        {
            DialogueManager.Instance.StartSingleLineDialogue(speakerName, firstPhaseMessage);
            return;
        }

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