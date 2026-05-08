using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PaintingPuzzle : MonoBehaviour
{
    [Header("UI")]
    public GameObject puzzlePanel;
    public RectTransform painting1;
    public RectTransform painting2;
    public RectTransform painting3;

    [Header("Reward")]
    public InventoryItemData usbItem;

    [Header("Dialogue")]
    public string speakerName = "Hazel";
    public string inspectMessage = "These paintings seem like they can rotate.";
    public string solvedMessage = "After rotating them in order, a USB drive fell out.";

    private int step1 = 0;
    private int step2 = 0;
    private int step3 = 0;
    private bool solved = false;

    private void Start()
    {
        if (puzzlePanel != null)
            puzzlePanel.SetActive(false);
    }

    private void Update()
    {
        if (solved) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                return;

            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

            foreach (Collider2D hit in hits)
            {
                if (hit.transform == transform || hit.transform.IsChildOf(transform))
                {
                    OpenPuzzle();
                    return;
                }
            }
        }

        if (puzzlePanel != null && puzzlePanel.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePuzzle();
        }
    }

    public void OpenPuzzle()
    {
        if (puzzlePanel != null)
            puzzlePanel.SetActive(true);

        DialogueManager.Instance.StartSingleLineDialogue(speakerName, inspectMessage);
    }

    public void RotatePainting1()
    {
        step1++;
        if (step1 > 7) step1 = 0;
        painting1.Rotate(0, 0, -90f);
        CheckPuzzle();
    }

    public void RotatePainting2()
    {
        step2++;
        if (step2 > 7) step2 = 0;
        painting2.Rotate(0, 0, -90f);
        CheckPuzzle();
    }

    public void RotatePainting3()
    {
        step3++;
        if (step3 > 7) step3 = 0;
        painting3.Rotate(0, 0, -90f);
        CheckPuzzle();
    }

    private void CheckPuzzle()
    {
        if (step1 == 5 && step2 == 2 && step3 == 7)
        {
            solved = true;

            InventoryManager.Instance.AddItem(usbItem);

            if (puzzlePanel != null)
                puzzlePanel.SetActive(false);

            DialogueManager.Instance.StartSingleLineDialogue(speakerName, solvedMessage);
        }
    }

    public void ClosePuzzle()
    {
        if (puzzlePanel != null)
            puzzlePanel.SetActive(false);

        ResetPuzzle();
    }
    private void ResetPuzzle()
    {
        step1 = 0;
        step2 = 0;
        step3 = 0;

        painting1.rotation = Quaternion.identity;
        painting2.rotation = Quaternion.identity;
        painting3.rotation = Quaternion.identity;
    }
}
