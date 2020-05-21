using UnityEngine;
using UnityEngine.EventSystems;

public class NodeDrag : MonoBehaviour, IBeginDragHandler,
    IEndDragHandler, IDragHandler, IPointerDownHandler
{
    [SerializeField] private Canvas guiCanvas;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = .6f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / guiCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1.0f;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }
}
