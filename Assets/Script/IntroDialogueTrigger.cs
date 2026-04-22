using UnityEngine;

public class IntroDialogueTrigger : MonoBehaviour
{
    public DialogueData introDialogueData;
    public bool hideAfterDialogue = true;

    private bool hasStarted = false;

    private void OnMouseDown()
    {
        if (GameProgress.introDialogueFinished || hasStarted)
            return;

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

    public void FinishIntroDialogue()
    {
        GameProgress.introDialogueFinished = true;

        if (hideAfterDialogue)
        {
            gameObject.SetActive(false);
        }
    }
}