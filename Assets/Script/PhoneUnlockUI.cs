using UnityEngine;
using TMPro;

public class PhoneUnlockUI : MonoBehaviour
{
    public GameObject phonePanel;
    public TMP_InputField passwordInput;
    public GameObject message;
    public TMP_Text messageText;

    public string correctPassword = "1951";
    public InventoryItemData phoneEvidence;

    private bool evidenceAdded = false;

    private void Start()
    {
        message.SetActive(false);
        messageText.text = "";
    }

    public void OpenPhone()
    {
        phonePanel.SetActive(true);
        passwordInput.text = "";


    }

    public void ClosePhone()
    {
        phonePanel.SetActive(false);
        message.SetActive(false);
    }

    public void TryUnlock()
    {
        message.SetActive(true); 

        if (passwordInput.text == correctPassword)
        {
            messageText.text = "Message found:\nA message conversation between Bob and Monica. Bob claims to have recorded evidence, possibly related to a scandal.";

            if (!evidenceAdded && InventoryManager.Instance != null && phoneEvidence != null)
            {
                InventoryManager.Instance.AddItem(phoneEvidence);
                evidenceAdded = true;
            }
        }
        else
        {
            messageText.text = "Wrong password.";
        }
    }
}
