using UnityEngine;
using UnityEngine.EventSystems;

public class NPCDialogueController : MonoBehaviour
{
    [Header("Dialogue")]
   
    public DialogueData introDialogue;
    public DialogueData evidenceChoiceDialogue;

[Header("Suspect Progress")]
public bool markGuestDialogueComplete = false;
public bool markAssistantDialogueComplete = false;
public bool markSupporterDialogueComplete = false;
    [Header("Repeat After Intro")]
    public string repeatSpeakerName = "Detective";

    [TextArea(2, 4)]
    public string repeatLineAfterIntro;

    [Header("Progress Marks")]
    public bool markIntroDialogueFinished = false;
    public bool markSoulDialogueComplete = false;

    [Header("Interaction")]
    public bool requirePlayerInRange = true;

    private bool playerInRange = false;
    private bool introFinished = false;

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

       if (!introFinished)
{
    DialogueManager.Instance.StartDialogue(introDialogue);
    introFinished = true;

    if (markSoulDialogueComplete)
    {
        GameProgress.soulDialogueComplete = true;
        Debug.Log("Soul dialogue complete!");
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