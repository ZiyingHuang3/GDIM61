using UnityEngine;
using UnityEngine.EventSystems;

public class NPCDialogueController : MonoBehaviour
{
    [Header("Dialogue")]
    [Header("Progress")]
    public bool markSoulDialogueComplete = false;

    public DialogueData introDialogue;
    public DialogueData evidenceChoiceDialogue;

    [Header("Repeat After Intro")]
    public string repeatSpeakerName = "Detective";

    [TextArea(2, 4)]
    public string repeatLineAfterIntro;

    private bool playerInRange = false;
    private bool introFinished = false;

private void Update()
{
    if (!playerInRange) return;

    if (Input.GetMouseButtonDown(0))
    {
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        bool clickedThisNPC = false;

        foreach (Collider2D hit in hits)
        {
            Debug.Log("Clicked collider: " + hit.gameObject.name);

            if (hit.GetComponentInParent<NPCDialogueController>() == this)
            {
                clickedThisNPC = true;
                break;
            }
        }

        if (!clickedThisNPC)
        {
            Debug.Log("Did not click this NPC.");
            return;
        }

        if (DialogueManager.Instance.IsDialogueActive)
        {
            Debug.Log("Dialogue is already active.");
            return;
        }

        if (!introFinished)
        {
            Debug.Log("Start intro dialogue.");
            DialogueManager.Instance.StartDialogue(introDialogue, null);
            introFinished = true;

            if (markSoulDialogueComplete)
            {
                GameProgress.soulDialogueComplete = true;
                Debug.Log("Soul dialogue complete!");
            }

            return;
        }

        if (InventoryManager.Instance != null && InventoryManager.Instance.HasAnyItem())
        {
            Debug.Log("Start evidence dialogue.");
            DialogueManager.Instance.StartDialogue(evidenceChoiceDialogue, null);
        }
        else
        {
            Debug.Log("Start repeat line.");
            DialogueManager.Instance.StartSingleLineDialogue(
                repeatSpeakerName,
                repeatLineAfterIntro
            );
        }
    }
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }


}