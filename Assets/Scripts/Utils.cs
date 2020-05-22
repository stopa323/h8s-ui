using UnityEngine;

public static class Utils 
{
    public static Vector2 ScreenToCanvasPosition(
        Vector2 screenPos, RectTransform canvasRt)
    {
        if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvasRt, screenPos, null, out Vector2 localCursor))
            return Vector2.zero;

        var xpos = localCursor.x + canvasRt.rect.width / 2;
        var ypos = localCursor.y - canvasRt.rect.height / 2;

        return new Vector2(xpos, ypos);
    }
}
