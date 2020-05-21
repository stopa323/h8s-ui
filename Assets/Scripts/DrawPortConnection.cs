using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;

public class DrawPortConnection : MonoBehaviour, IPointerDownHandler, IBeginDragHandler,
    IEndDragHandler, IDragHandler, IDropHandler
{
    [SerializeField] private UILineRenderer bezier;
    [SerializeField] private RectTransform nodeRt;
    [SerializeField] private Canvas guiCanvas;
    [SerializeField] private RectTransform canvasRt;

    private const float PORT_ANCHOR = 15f;

    private RectTransform rt;
    private Vector2 portAnchor = new Vector2(PORT_ANCHOR, PORT_ANCHOR);

    private void Awake()
    {
        bezier.enabled = false;

        rt = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("OnBeginDrag");
        bezier.enabled = true;

        reset();
    }

    public void OnDrag(PointerEventData eventData)
    {
        var move_vector = eventData.delta / guiCanvas.scaleFactor;
        rt.anchoredPosition += move_vector;
        portAnchor -= move_vector;

        updateBezierCurve();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log("OnEndDrag");
        bezier.enabled = false;

        reset();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log(name);
    }

    private void updateBezierCurve()
    {
        var points = new List<Vector2>() {
            new Vector2(PORT_ANCHOR, -PORT_ANCHOR),
            new Vector2(.5f * (PORT_ANCHOR + portAnchor.x), -PORT_ANCHOR),
            new Vector2(.5f * (PORT_ANCHOR + portAnchor.x), portAnchor.y - 2 * PORT_ANCHOR),
            new Vector2(portAnchor.x, portAnchor.y - 2 * PORT_ANCHOR)
        };
        bezier.Points = points.ToArray();
    }

    private void reset()
    {
        rt.anchoredPosition = Vector2.zero;
        portAnchor = new Vector2(PORT_ANCHOR, PORT_ANCHOR);
        updateBezierCurve();
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log(name);
    }
}
