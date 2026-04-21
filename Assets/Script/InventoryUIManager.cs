using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUIManager : MonoBehaviour
{
    public static InventoryUIManager Instance;

    [Header("Main UI")]
    public GameObject inventoryPanel;

    [Header("Detail UI")]
    public TMP_Text itemNameText;
    public TMP_Text itemDescriptionText;
    public Image itemIconImage;

    [Header("List UI")]
    public Transform itemListParent;
    public GameObject itemButtonPrefab;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        inventoryPanel.SetActive(false);
    }

    public void ToggleInventory()
    {
        bool isOpen = inventoryPanel.activeSelf;
        inventoryPanel.SetActive(!isOpen);

        if (!isOpen)
        {
            RefreshInventoryUI();
        }
    }

    public void RefreshInventoryUI()
    {
        foreach (Transform child in itemListParent)
        {
            Destroy(child.gameObject);
        }

        if (InventoryManager.Instance == null) return;

        foreach (InventoryItemData item in InventoryManager.Instance.items)
        {
            GameObject buttonObj = Instantiate(itemButtonPrefab, itemListParent);
            InventoryItemButton button = buttonObj.GetComponent<InventoryItemButton>();
            button.Setup(item);
        }
    }

    public void ShowItemDetail(InventoryItemData item)
    {
        if (item == null) return;

        itemNameText.text = item.itemName;
        itemDescriptionText.text = item.description;

        if (itemIconImage != null)
        {
            itemIconImage.sprite = item.icon;
            itemIconImage.enabled = item.icon != null;
        }
    }
    public void CloseInventory()
    {
        inventoryPanel.SetActive(false);
    }
}
