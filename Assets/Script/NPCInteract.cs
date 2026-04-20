using UnityEngine;

public class NPCInteract : MonoBehaviour
{
    public GameObject interactHint;   // “Press Space” 提示
    public DialogueData dialogueData;

    private bool playerInRange = false;

    private void Start()
    {
        if (interactHint != null)
            interactHint.SetActive(false);
    }

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!DialogueManager.Instance.IsDialogueActive)
            {
                DialogueManager.Instance.StartDialogue(dialogueData, this);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (interactHint != null && !DialogueManager.Instance.IsDialogueActive)
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
        if (playerInRange && interactHint != null && !DialogueManager.Instance.IsDialogueActive)
            interactHint.SetActive(true);
    }
}