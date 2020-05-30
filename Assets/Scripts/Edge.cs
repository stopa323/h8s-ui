using UnityEngine;

namespace h8s
{
    public class Edge : MonoBehaviour
    {
        public PortBase SourcePort { get; private set; }
        public PortBase DestinationPort { get; private set; }

        private EdgeDrawer drawer;

        private void Awake()
        {
            drawer = GetComponent<EdgeDrawer>();
        }

        public void Initialize(Vector2 initPosition, DataType dataType)
        {
            drawer.Initialize(initPosition, dataType);
        }

        public void Bind(PortBase srcPort, PortBase dstPort)
        {
            SourcePort = srcPort;
            DestinationPort = dstPort;

            SourcePort.Bind(this);
            DestinationPort.Bind(this);
            // Notify SchemeManager about new edge
        }

        public void MoveEndAnchor(Vector2 newPosition)
        {
            drawer.MoveEndAnchor(newPosition);
        }
    }
}