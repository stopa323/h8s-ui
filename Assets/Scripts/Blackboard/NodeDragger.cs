using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class NodeDragger : MonoBehaviour, IBeginDragHandler,
        IEndDragHandler, IDragHandler
    {
        private RectTransform rectTransform;
        private CanvasGroup canvasGroup;
        private Node parentNode;

        private void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
            canvasGroup = GetComponent<CanvasGroup>();
            parentNode = GetComponent<Node>();
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = .6f;
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / BlackboardManager.GUICanvas.scaleFactor;
            parentNode.OnNodeMove(eventData.delta / BlackboardManager.GUICanvas.scaleFactor);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1.0f;
        }
    }
}
