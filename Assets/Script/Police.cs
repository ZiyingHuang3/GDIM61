using UnityEngine;
using UnityEngine.EventSystems;

public class PoliceNPC : MonoBehaviour
{
    public InventoryItemData evidence1;
    public InventoryItemData evidence2;

    private bool hasGivenEvidence = false;

    private void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (hasGivenEvidence)
            return;

        GivePoliceEvidence();
    }

    void GivePoliceEvidence()
    {
        hasGivenEvidence = true;

        GameProgress.policeEvidenceComplete = true;

        InventoryManager.Instance.AddItem(evidence1);
        InventoryManager.Instance.AddItem(evidence2);

        Debug.Log("Police evidence collected.");
    }
}