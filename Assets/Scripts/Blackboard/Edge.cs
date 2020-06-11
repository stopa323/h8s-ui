using UnityEngine;

namespace h8s
{
    public class Edge : MonoBehaviour
    {
        public Port SourcePort { get; private set; }
        public Port DestinationPort { get; private set; }

        private EdgeDrawer drawer;

        private void Awake()
        {
            drawer = GetComponent<EdgeDrawer>();
        }

        public void Initialize(Vector2 initPosition, DataType dataType)
        {
            drawer.Initialize(initPosition, dataType);
        }

        public void Bind(Port srcPort, Port dstPort)
        {
            SourcePort = srcPort;
            DestinationPort = dstPort;

            SourcePort.Bind(this);
            DestinationPort.Bind(this);
            // Notify SchemeManager about new edge
        }

        private void OnDestroy()
        {
            if (SourcePort)
            {
                SourcePort.Unbind();
            }

            if (DestinationPort)
            {
                DestinationPort.Unbind();
            }
        }

        public void MoveEndAnchor(Vector2 newPosition)
        {
            drawer.MoveEndAnchor(newPosition);
        }

        public void OnPortMoved(Port port, Vector2 shift)
        {
            if (port == SourcePort) { drawer.ShiftStartAnchor(shift); }
            else { drawer.ShiftEndAnchor(shift); }
        }
    }
}