using UnityEngine;

public class IntroDialogueTrigger : MonoBehaviour
{
    public DialogueData introDialogueData;
    public bool hideAfterDialogue = true;

    private bool hasStarted = false;
    private bool playerInRange = false;

    private void Update()
{
    if (GameProgress.introDialogueFinished || hasStarted)
        return;

    if (!playerInRange) return;

    if (Input.GetMouseButtonDown(0))
    {
        if (UnityEngine.EventSystems.EventSystem.current != null &&
            UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.gameObject == gameObject)
        {
            if (DialogueManager.Instance == null)
            {
                Debug.LogError("DialogueManager.Instance is null");
                return;
            }

            if (introDialogueData == null)
            {
                Debug.LogError("introDialogueData is null on " + gameObject.name);
                return;
            }

            hasStarted = true;
            DialogueManager.Instance.StartDialogue(introDialogueData, null);
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

    public void FinishIntroDialogue()
    {
        GameProgress.introDialogueFinished = true;

        if (hideAfterDialogue)
        {
            gameObject.SetActive(false);
        }
    }
}