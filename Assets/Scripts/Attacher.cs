using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class Attacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IDropHandler
    {
        [SerializeField] public Port ParentPort;

        [SerializeField] private GameObject edgePrefab;

        /* Reference to Edge that has been successfully bound */
        private Edge boundEdge;             

        /* Reference to Edge that is durrently being dragged */
        private Edge drawingEdge;          

        private bool discardAttachment;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var edge = Instantiate(edgePrefab, SchemeManager.GUICanvas.transform);
            drawingEdge = edge.GetComponent<Edge>();
            drawingEdge.Initialize(Utils.ScreenToCanvasPosition(eventData.pressPosition),
                ParentPort.PortType);

            // Discard by default, it is changed in case certain contitions are met
            discardAttachment = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            drawingEdge.MoveEndAnchor(Utils.ScreenToCanvasPosition(eventData.position));
        }

        public void OnDrop(PointerEventData eventData)
        {
            var srcPort = eventData.pointerDrag.GetComponent<Attacher>().ParentPort;
            var dstPort = eventData.pointerEnter.GetComponent<Attacher>().ParentPort;

            Debug.Log(srcPort.PortDirection != dstPort.PortDirection);
            Debug.Log(srcPort.PortType == dstPort.PortType);
            Debug.Log(srcPort != dstPort);

            if (srcPort.PortDirection != dstPort.PortDirection &&
                srcPort.PortType == dstPort.PortType &&
                srcPort != dstPort)
            {
                eventData.pointerDrag.GetComponent<Attacher>().ConfirmEdgeAttachment();
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (discardAttachment)
            {
                Destroy(drawingEdge.gameObject);
            }
        }

        public void ConfirmEdgeAttachment()
        {
            discardAttachment = false;
        }
    }

}