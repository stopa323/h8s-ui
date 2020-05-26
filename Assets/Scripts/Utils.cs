using UnityEngine;

namespace h8s
{
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

        public static Color32 GetDataTypeColor(DataType dataType)
        {
            var defaultColor = new Color32(255, 255, 255, 255);

            switch (dataType)
            {
                case DataType.Exec:
                    return new Color32(255, 255, 255, 255);
                case DataType.String:
                    return new Color32(251, 197, 49, 255);
                case DataType.Bool:
                    return new Color32(207, 51, 13, 255);
                case DataType.Object:
                    return new Color32(0, 168, 255, 255);
                default:
                    return defaultColor;
            }
        }
    }
}