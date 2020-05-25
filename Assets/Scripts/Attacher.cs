using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI.Extensions;
using h8s.objects;
using System.Collections.Generic;

public class Attacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, 
    IDropHandler
{
    [SerializeField] private Port parentPort;

    [SerializeField] private GameObject edgePrefab;
    [SerializeField] private GameObject edgeConnectorPrefab;

    private Edge boundEdge;             // Edge that is currently 2-way attached
    private Edge drawingEdge;           // Edge that is being drawned on dragging

    public void OnBeginDrag(PointerEventData eventData)
    {
        var edge = Instantiate(edgePrefab, SchemeManager.GUICanvas.transform);
        drawingEdge = edge.GetComponent<Edge>();
        drawingEdge.Initialize(Utils.ScreenToCanvasPosition(eventData.pressPosition));
    }

    public void OnDrag(PointerEventData eventData)
    {
        drawingEdge.MoveEndAnchor(Utils.ScreenToCanvasPosition(eventData.position));
    }

    public void OnDrop(PointerEventData eventData)
    {
        var srcPort = eventData.pointerDrag.GetComponent<Port>();
        var dstPort = eventData.pointerDrag.GetComponent<Port>();

        //if ()
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        
    }
}
