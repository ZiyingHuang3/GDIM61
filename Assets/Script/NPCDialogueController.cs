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
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hit = Physics2D.OverlapPoint(mousePos);

            if (hit == null || hit.gameObject != gameObject)
                return;

            if (DialogueManager.Instance.IsDialogueActive) return;

        if (!introFinished)
{
    Debug.Log("NPC: first intro dialogue");
    DialogueManager.Instance.StartDialogue(introDialogue, null);
    introFinished = true;
    return;
}

if (InventoryManager.Instance != null && InventoryManager.Instance.HasAnyItem())
{
    Debug.Log("NPC: has evidence, start evidence choice");
    DialogueManager.Instance.StartDialogue(evidenceChoiceDialogue, null);
}
else
{
    Debug.Log("NPC: no evidence, repeat line = " + repeatLineAfterIntro);
    DialogueManager.Instance.StartSingleLineDialogue(
        repeatSpeakerName,
        repeatLineAfterIntro
    );
}

            if (InventoryManager.Instance != null && InventoryManager.Instance.HasAnyItem())
            {
                DialogueManager.Instance.StartDialogue(evidenceChoiceDialogue, null);
            }
            else
            {
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

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}