using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryItemButton : MonoBehaviour
{
    public TMP_Text itemText;
    public Image iconImage;

    private InventoryItemData itemData;
    private Button button;

    public void Setup(InventoryItemData item)
    {
        if (item == null)
        {
            Debug.LogError("item is NULL on " + gameObject.name);
            return;
        }

        if (itemText == null)
        {
            Debug.LogError("itemText is NULL on " + gameObject.name);
            return;
        }

        if (button == null)
            button = GetComponent<Button>();

        if (button == null)
        {
            Debug.LogError("Button is STILL NULL on " + gameObject.name);
            return;
        }

        itemData = item;
        itemText.text = "";

        if (iconImage != null)
        {
            iconImage.sprite = item.icon;
            iconImage.enabled = item.icon != null;
            iconImage.preserveAspect = true;
        }

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(OnClickItem);
    }

    private void OnClickItem()
    {
        if (InventoryUIManager.Instance != null)
        {
            InventoryUIManager.Instance.ShowItemDetail(itemData);
        }
    }
}
