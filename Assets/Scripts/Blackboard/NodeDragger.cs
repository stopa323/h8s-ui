using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class NodeDragger : MonoBehaviour, IBeginDragHandler,
        IEndDragHandler, IDragHandler
    {
        [Header("Drag parameters")]
        [SerializeField] private const float ALPHA_FACTOR = .8f;
        [SerializeField] private const float SCALE_FACTOR = .9f;
        [SerializeField] private const float SCALE_ANIMATION_TIME = .1f;

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
            canvasGroup.alpha = ALPHA_FACTOR;
            StartCoroutine(ScaleAnimation());
        }

        public void OnDrag(PointerEventData eventData)
        {
            rectTransform.anchoredPosition += eventData.delta / BlackboardManager.GUICanvas.scaleFactor;
            parentNode.OnNodeMove(eventData.delta / BlackboardManager.GUICanvas.scaleFactor);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            canvasGroup.alpha = 1.0f;
            StartCoroutine(ScaleAnimation(true));
        }

        IEnumerator ScaleAnimation(bool increase = false)
        {
            var scale = increase ? 1f : SCALE_FACTOR;
            var srcScale = rectTransform.localScale;
            var destScale = new Vector3(scale, scale);
            var elapsedTime = 0f;

            while (elapsedTime < SCALE_ANIMATION_TIME)
            {
                rectTransform.localScale = Vector3.Lerp(srcScale, destScale, (elapsedTime / SCALE_ANIMATION_TIME));
                elapsedTime += Time.deltaTime;

                yield return null;
            }

            rectTransform.localScale = destScale;
            yield return null;
        }
    }
}
