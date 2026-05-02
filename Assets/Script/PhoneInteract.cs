using UnityEngine;
using UnityEngine.EventSystems;

public class PhoneInteract : MonoBehaviour
{
    public PhoneUnlockUI phoneUI;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0)) return;

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mousePos);

        if (hit != null && hit.gameObject == gameObject)
        {
            Debug.Log("Phone clicked");

            if (phoneUI == null)
            {
                Debug.LogError("phoneUI is null");
                return;
            }

            phoneUI.OpenPhone();
        }
    }
}