using UnityEngine;
using UnityEngine.UI;

namespace h8s
{ 
    public class Port : MonoBehaviour
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        public Node ParentNode { get; private set; }
        public Edge ConnectedEdge { get; private set; }
        public string Name { get { return nameField.text; } private set { nameField.text = value; } }
        public PortDirection Direction { get; private set; }
        public DataType Type { get; private set; }

        private bool isInitialized = false;

        public void Initialize(Node parent, PortDirection direction, DataType type, string name)
        {
            if (isInitialized) throw new UnityException(string.Format(
                "Attempt to initialize already initialized Port: {0}@{1}", 
                Name, ParentNode.name));

            ParentNode = parent;
            Direction = direction;
            Type = type;
            iconField.color = Utils.GetDataTypeColor(type);
            Name = name;

            isInitialized = true;
        }

        public void Bind(Edge edge)
        {
            if (ConnectedEdge)
            {
                // Notify SchemeManager about this destroy
                Destroy(ConnectedEdge.gameObject);
            }

            ConnectedEdge = edge;
        }
    }
}
