using UnityEngine;
using UnityEngine.EventSystems;

public class NPCInteract : MonoBehaviour
{
    public GameObject interactHint;
    public DialogueData dialogueData;

    private bool playerInRange = false;

    private void Start()
    {
        if (interactHint != null)
            interactHint.SetActive(false);
    }

    private void Update()
    {
        if (!playerInRange) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (InventoryUIManager.Instance != null &&
                InventoryUIManager.Instance.inventoryPanel.activeSelf)
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider != null && hit.collider.gameObject == gameObject)
            {
                if (DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive)
                {
                    DialogueManager.Instance.StartDialogue(dialogueData, this);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactHint != null && DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive)
                interactHint.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (interactHint != null)
                interactHint.SetActive(false);
        }
    }

    public void HideHint()
    {
        if (interactHint != null)
            interactHint.SetActive(false);
    }

    public void ShowHintIfPlayerInRange()
    {
        if (playerInRange && interactHint != null && DialogueManager.Instance != null && !DialogueManager.Instance.IsDialogueActive)
            interactHint.SetActive(true);
    }
}