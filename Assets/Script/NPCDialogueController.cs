using UnityEngine;
using UnityEngine.EventSystems;

public class NPCDialogueController : MonoBehaviour
{
    [Header("Dialogue")]
    public DialogueData introDialogue;
    public DialogueData evidenceChoiceDialogue;

    [Header("Deduction Dialogue After Part 2")]
    public bool useDeductionDialogueAfterPart2 = false;
    public DialogueData deductionDialogue;

    [Header("Suspect Progress")]
    public bool markGuestDialogueComplete = false;
    public bool markAssistantDialogueComplete = false;
    public bool markSupporterDialogueComplete = false;

    [Header("Repeat After Intro")]
    public string repeatSpeakerName = "Detective";

    [Header("Second Phase Repeat")]
    public bool repeatOnlyAfterReturnedToMap1 = false;

    [TextArea(2, 4)]
    public string repeatLineAfterIntro;

    [Header("Progress Marks")]
    public bool markIntroDialogueFinished = false;
    public bool markSoulDialogueComplete = false;

    [Header("Interaction")]
    public bool requirePlayerInRange = true;

    private bool playerInRange = false;
    private bool introFinished = false;

    [Header("Evidence Required Dialogue")]
    public InventoryItemData requiredEvidence;
    public DialogueData afterEvidenceDialogue;

    private void Update()
    {
        if (requirePlayerInRange && !playerInRange) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            if (!ClickedThisNPC())
                return;

            TryStartDialogue();
        }
    }

    private bool ClickedThisNPC()
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        foreach (Collider2D hit in hits)
        {
            if (hit.GetComponentInParent<NPCDialogueController>() == this)
                return true;
        }

        return false;
    }

    private void TryStartDialogue()
    {
        if (DialogueManager.Instance == null) return;
        if (DialogueManager.Instance.IsDialogueActive) return;

        if (useDeductionDialogueAfterPart2 &&
            GameProgress.part2EvidenceComplete &&
            deductionDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(deductionDialogue);
            return;
        }

        if (repeatOnlyAfterReturnedToMap1 && GameProgress.returnedToMap1)
        {
            DialogueManager.Instance.StartSingleLineDialogue(
                repeatSpeakerName,
                repeatLineAfterIntro
            );
            return;
        }

        if (!introFinished)
        {
            DialogueManager.Instance.StartDialogue(introDialogue);
            introFinished = true;

            if (markIntroDialogueFinished)
            {
                GameProgress.introDialogueFinished = true;
            }

            if (markSoulDialogueComplete)
            {
                GameProgress.soulDialogueComplete = true;
            }

            if (markGuestDialogueComplete)
            {
                GameProgress.guestDialogueComplete = true;
                Debug.Log("Guest dialogue complete!");
            }

            if (markAssistantDialogueComplete)
            {
                GameProgress.assistantDialogueComplete = true;
                Debug.Log("Assistant dialogue complete!");
            }

            if (markSupporterDialogueComplete)
            {
                GameProgress.supporterDialogueComplete = true;
                Debug.Log("Supporter dialogue complete!");
            }

            return;
        }

        if (InventoryManager.Instance != null &&
            requiredEvidence != null &&
            InventoryManager.Instance.items.Contains(requiredEvidence) &&
            afterEvidenceDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(afterEvidenceDialogue);
        }
        else if (InventoryManager.Instance != null &&
                 InventoryManager.Instance.HasAnyItem() &&
                 evidenceChoiceDialogue != null)
        {
            DialogueManager.Instance.StartDialogue(evidenceChoiceDialogue);
        }
        else
        {
            DialogueManager.Instance.StartSingleLineDialogue(
                repeatSpeakerName,
                repeatLineAfterIntro
            );
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}