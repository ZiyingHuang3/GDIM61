using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : MonoBehaviour
{
    public Texture2D normalCursor;
    public Texture2D magnifierCursor;
    public LayerMask investigableLayer;

    void Update()
    {
        if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
        {
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            return;
        }

        Vector2 mouseWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D hit = Physics2D.OverlapPoint(mouseWorld, investigableLayer);

        if (hit != null)
            Cursor.SetCursor(magnifierCursor, Vector2.zero, CursorMode.Auto);
        else
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
    }
}
