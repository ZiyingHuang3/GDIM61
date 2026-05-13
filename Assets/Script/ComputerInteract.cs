using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class ComputerInteract : MonoBehaviour
{
    public GameObject computerPanel;
    public InventoryItemData usbItem;
    [Header("Dialogue")]
    public string speakerName = "Hazel";
    public string inspectMessage = "These paintings seem like they can rotate.";
    public string solvedMessage = "After rotating them in order, a USB drive fell out.";
    public InventoryItemData recordingEvidence;

    private bool evidenceAdded = false;
    private void Start()
    {
        if (computerPanel != null)
            computerPanel.SetActive(false);
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
                OpenComputer();
                return;
            }
        }
    }

    public void OpenComputer()
    {
        computerPanel.SetActive(true);
    }

    public void OpenApp()
    {
        if (InventoryManager.Instance != null &&
            InventoryManager.Instance.HasItem(usbItem))
        {
            DialogueManager.Instance.StartSingleLineDialogue(
                speakerName,
                solvedMessage
            );

            if (!evidenceAdded)
            {
                InventoryManager.Instance.AddItem(recordingEvidence);
                evidenceAdded = true;
            }
        }
        else
        {
            DialogueManager.Instance.StartSingleLineDialogue(
                speakerName,
                inspectMessage
            );
        }
    }

    public void CloseComputer()
    {
        computerPanel.SetActive(false);
    }
}