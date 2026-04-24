using UnityEngine;

public class NPCDialogueController : MonoBehaviour
{
    [Header("Dialogue")]
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
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (DialogueManager.Instance.IsDialogueActive) return;

            if (!introFinished)
            {
                DialogueManager.Instance.StartDialogue(introDialogue, null);
                introFinished = true;
                return;
            }

            if (InventoryManager.Instance != null && InventoryManager.Instance.HasAnyItem())
            {
                DialogueManager.Instance.StartDialogue(evidenceChoiceDialogue,null);
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
