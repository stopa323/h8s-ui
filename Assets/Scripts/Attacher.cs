using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class Attacher : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler,
        IDropHandler
    {
        [SerializeField] public PortBase ParentPort;

        [SerializeField] private GameObject edgePrefab;            

        /* Reference to Edge that is durrently being dragged */
        public Edge DrawingEdge { get; private set; }         

        private bool discardEdge;

        public void OnBeginDrag(PointerEventData eventData)
        {
            var edge = Instantiate(edgePrefab, SchemeManager.GUICanvas.transform);
            DrawingEdge = edge.GetComponent<Edge>();
            DrawingEdge.Initialize(Utils.ScreenToCanvasPosition(eventData.pressPosition),
                ParentPort.Type);

            // Discard by default, it is changed in case certain contitions are met
            discardEdge = true;
        }

        public void OnDrag(PointerEventData eventData)
        {
            DrawingEdge.MoveEndAnchor(Utils.ScreenToCanvasPosition(eventData.position));
        }

        public void OnDrop(PointerEventData eventData)
        {
            var srcAttacher = eventData.pointerDrag.GetComponent<Attacher>();
            var srcPort = srcAttacher.ParentPort;
            var dstPort = eventData.pointerEnter.GetComponent<Attacher>().ParentPort;

            if (srcPort.Direction != dstPort.Direction &&
                srcPort.Type == dstPort.Type &&
                srcPort.ParentNode != dstPort.ParentNode) // Restrict connection within same node
            {
                Debug.LogFormat("Passed binding validation between {0}@{1} : {2}@{3}", 
                    srcPort.Name, srcPort.ParentNode.name, dstPort.Name, dstPort.ParentNode.name);

                srcAttacher.SupressEdgeDiscard();

                // Todo: How do i know what is src/dst?
                srcAttacher.DrawingEdge.Bind(srcPort, dstPort);
                DrawingEdge = null;
            }
        }

        public void OnEndDrag(PointerEventData eventData) { if (discardEdge) { Destroy(DrawingEdge.gameObject); } }

        /* Tell source that destination Attacher will handle connection */ 
        public void SupressEdgeDiscard() { discardEdge = false; }
    }

}