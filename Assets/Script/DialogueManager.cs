using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public TMP_Text speakerNameText;
    private int allChoicesCompletedNextNode = -1;
    private HashSet<int> visitedNodes = new HashSet<int>();

    [Header("Dialogue UI")]
    public GameObject dialoguePanel;
    public TMP_Text dialogueText;

    [Header("Choice UI")]
    public GameObject choicePanel;
    public Button[] choiceButtons;
    public TMP_Text[] choiceTexts;

    public bool IsDialogueActive { get; private set; }

    private DialogueData currentDialogueData;
    private int currentNodeIndex;
    private bool waitingForChoice = false;
    private bool ignoreNextClick = false;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);
    }

private void Update()
{
    if (!IsDialogueActive) return;

    if (Input.GetMouseButtonDown(0))
    {
        if (ignoreNextClick)
        {
            ignoreNextClick = false;
            return;
        }

        if (currentDialogueData == null)
        {
            EndDialogue();
            return;
        }

        if (!waitingForChoice)
        {
            GoNext();
        }
    }
}

    public void StartDialogue(DialogueData data)
{
    
    Debug.Log("StartDialogue entered");

    if (data == null)
    {
        Debug.LogError("StartDialogue: data is null");
        return;
    }

    Debug.Log("StartDialogue: node count = " + data.nodes.Count);

    if (data.nodes.Count == 0)
    {
        Debug.LogError("StartDialogue: data.nodes.Count == 0");
        return;
    }
    Player player = FindObjectOfType<Player>();
if (player != null)
    player.enabled = false;

    IsDialogueActive = true;
    currentDialogueData = data;
    currentNodeIndex = 0;
    waitingForChoice = false;

    visitedNodes.Clear();
    allChoicesCompletedNextNode = -1;

    Debug.Log("Activating dialoguePanel");
    dialoguePanel.SetActive(true);
    choicePanel.SetActive(false);


    ShowCurrentNode();
}

public void StartSingleLineDialogue(string speakerName, string line)
{
    
    ignoreNextClick = true;
    IsDialogueActive = true;
    currentDialogueData = null;
    waitingForChoice = false;

    dialoguePanel.SetActive(true);
    choicePanel.SetActive(false);

    speakerNameText.text = speakerName;
    dialogueText.text = line;
    Player player = FindObjectOfType<Player>();
if (player != null)
    player.enabled = false;
}

    private void ShowCurrentNode()
    {
        if (currentDialogueData == null ||
            currentNodeIndex < 0 ||
            currentNodeIndex >= currentDialogueData.nodes.Count)
        {
            EndDialogue();
            return;
        }

        DialogueNode node = currentDialogueData.nodes[currentNodeIndex];
        speakerNameText.text = node.speakerName;
        dialogueText.text = node.dialogueText;

        if (node.hasChoices && node.choices != null && node.choices.Count > 0)
        {
            waitingForChoice = true;
            allChoicesCompletedNextNode = node.afterAllChoicesNextNode;
            ShowChoices(node.choices);
        }
        else
        {
            waitingForChoice = false;
            choicePanel.SetActive(false);
        }
    }

    private void ShowChoices(List<DialogueChoice> choices)
    {
        choicePanel.SetActive(true);

        int buttonIndex = 0;

        for (int i = 0; i < choices.Count; i++)
        {
            DialogueChoice choice = choices[i];

            if (visitedNodes.Contains(choice.nextNodeIndex))
                continue;

            if (choice.requiredEvidence != null &&
                (InventoryManager.Instance == null ||
                 !InventoryManager.Instance.items.Contains(choice.requiredEvidence)))
            {
                continue;
            }

            if (buttonIndex >= choiceButtons.Length)
                break;

            choiceButtons[buttonIndex].gameObject.SetActive(true);
            choiceTexts[buttonIndex].text = choice.choiceText;

            int nextIndex = choice.nextNodeIndex;
            choiceButtons[buttonIndex].onClick.RemoveAllListeners();
            choiceButtons[buttonIndex].onClick.AddListener(() => SelectChoice(nextIndex));

            buttonIndex++;
        }

        for (int i = buttonIndex; i < choiceButtons.Length; i++)
        {
            choiceButtons[i].gameObject.SetActive(false);
        }

        if (buttonIndex == 0)
        {
            choicePanel.SetActive(false);
            waitingForChoice = false;
            EndDialogue();
        }
    }

    private void SelectChoice(int nextNodeIndex)
    {
       visitedNodes.Add(nextNodeIndex);

       waitingForChoice = false;
       choicePanel.SetActive(false);

       currentNodeIndex = nextNodeIndex;
       ShowCurrentNode();
    }

private void GoNext()
{
    if (currentDialogueData == null)
    {
        EndDialogue();
        return;
    }

    DialogueNode node = currentDialogueData.nodes[currentNodeIndex];

    if (node.nextNodeIndex == -1)
    {
        EndDialogue();
        return;
    }

    currentNodeIndex = node.nextNodeIndex;
    ShowCurrentNode();
}


    public void EndDialogue()
{
    Debug.Log("EndDialogue called");

    if (currentDialogueData != null)
    {
        Debug.Log("currentDialogueData exists");
        Debug.Log("isIntroDialogue = " + currentDialogueData.isIntroDialogue);
    }
    else
    {
        Debug.Log("currentDialogueData is null");
    }

if (currentDialogueData != null && currentDialogueData.isIntroDialogue)
{
    Debug.Log("Intro dialogue finished!");
    GameProgress.introDialogueFinished = true;
}

    Debug.Log("introDialogueFinished = " + GameProgress.introDialogueFinished);

    IsDialogueActive = false;
    waitingForChoice = false;
    currentDialogueData = null;
    currentNodeIndex = 0;

    dialoguePanel.SetActive(false);
    choicePanel.SetActive(false);

   
    Player player = FindObjectOfType<Player>();
if (player != null)
    player.enabled = true;
}
}
