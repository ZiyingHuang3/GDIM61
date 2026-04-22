using UnityEngine;

public class IntroDialogueTrigger : MonoBehaviour
{
    public DialogueData introDialogueData;
    public bool hideAfterDialogue = true;
    public bool startAutomatically = true;

    private bool hasStarted = false;

    private void Start()
    {
        Debug.Log("IntroDialogueTrigger Start on " + gameObject.name);

        if (!startAutomatically || GameProgress.introDialogueFinished || hasStarted)
        {
            Debug.Log("Intro skipped");
            return;
        }

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

        Debug.Log("Starting intro dialogue...");
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