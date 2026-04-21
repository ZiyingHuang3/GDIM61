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
    private NPCInteract currentNPC;
    private bool waitingForChoice = false;

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

        if (!waitingForChoice && Input.GetMouseButtonDown(0))
        {
            GoNext();
        }
    }

    public void StartDialogue(DialogueData data, NPCInteract npc)
    {
        if (data == null || data.nodes.Count == 0) return;

        IsDialogueActive = true;
        currentDialogueData = data;
        currentNodeIndex = 0;
        currentNPC = npc;
        waitingForChoice = false;

        visitedNodes.Clear();
        allChoicesCompletedNextNode = -1;

        dialoguePanel.SetActive(true);
        choicePanel.SetActive(false);

        if (currentNPC != null)
            currentNPC.HideHint();

        ShowCurrentNode();
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
        int activeCount = 0;

        for (int i = 0; i < choiceButtons.Length; i++)
        {
            if (i < choices.Count)
            {
                choiceButtons[i].gameObject.SetActive(true);
                choiceTexts[i].text = choices[i].choiceText;

                int nextIndex = choices[i].nextNodeIndex;
                choiceButtons[i].onClick.RemoveAllListeners();
                choiceButtons[i].onClick.AddListener(() => SelectChoice(nextIndex));

                activeCount++;
            }
            else
            {
                choiceButtons[i].gameObject.SetActive(false);
            }
        }

        if (activeCount == 0)
        {
            choicePanel.SetActive(false);
            waitingForChoice = false;

            if (allChoicesCompletedNextNode != -1)
            {
                currentNodeIndex = allChoicesCompletedNextNode;
                ShowCurrentNode();
            }
            else
            {
                EndDialogue();
            }
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
        if (currentDialogueData == null) return;

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
        IsDialogueActive = false;
        waitingForChoice = false;
        currentDialogueData = null;
        currentNodeIndex = 0;

        dialoguePanel.SetActive(false);
        choicePanel.SetActive(false);

        if (currentNPC != null)
        {
            currentNPC.ShowHintIfPlayerInRange();
            currentNPC = null;
        }
    }
}
