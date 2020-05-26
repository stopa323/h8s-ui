using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

namespace h8s
{
    [RequireComponent(typeof(UILineRenderer))]
    public class EdgeDrawer : MonoBehaviour
    {
        /* Edge visual stuff handling */

        private UILineRenderer lr;

        private Vector2 startAnchorPosition;
        private Vector2 endAnchorPosition;

        private void Awake()
        {
            lr = GetComponent<UILineRenderer>();
        }

        public void Initialize(Vector2 anchor, DataType portType)
        {
            startAnchorPosition = anchor;
            endAnchorPosition = anchor;

            lr.color = Utils.GetDataTypeColor(portType);

            Repaint();
        }

        public void MoveStartAnchor(Vector2 newPosition)
        {
            startAnchorPosition = newPosition;
            Repaint();
        }

        public void MoveEndAnchor(Vector2 newPosition)
        {
            endAnchorPosition = newPosition;
            Repaint();
        }

        private void Repaint()
        {
            /* Update LineRenderer points position */
            var points = new List<Vector2>() {
            startAnchorPosition,
            new Vector2(0.5f * (startAnchorPosition.x + endAnchorPosition.x), startAnchorPosition.y),
            new Vector2(0.5f * (startAnchorPosition.x + endAnchorPosition.x), endAnchorPosition.y),
            endAnchorPosition
        };
            lr.Points = points.ToArray();
        }
    }

}