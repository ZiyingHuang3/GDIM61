using UnityEngine;
using UnityEngine.EventSystems;

public class OutlineHover : MonoBehaviour
{
    public GameObject outlineObject;

    private bool isHovering = false;

    private void Start()
    {
        if (outlineObject != null)
            outlineObject.SetActive(false);
    }

    private void Update()
    {
        if (outlineObject == null) return;

        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            SetOutline(false);
            return;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);

        bool hoveringThis = false;

        foreach (Collider2D hit in hits)
        {
            if (hit.transform.root == transform.root)
            {
                hoveringThis = true;
                break;
            }
        }

        SetOutline(hoveringThis);
    }

    private void SetOutline(bool show)
    {
        if (isHovering == show) return;

        isHovering = show;
        outlineObject.SetActive(show);
    }
}