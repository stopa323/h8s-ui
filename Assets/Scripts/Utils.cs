using UnityEngine;

public static class Utils 
{
    public static Vector2 ScreenToCanvasPosition(Vector2 screenPos)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            SchemeManager.GUICanvasRt, screenPos, null, out Vector2 localCursor))
            return Vector2.zero;

        var xpos = localCursor.x + SchemeManager.GUICanvasRt.rect.width / 2;
        var ypos = localCursor.y - SchemeManager.GUICanvasRt.rect.height / 2;

        return new Vector2(xpos, ypos);
    }
}
