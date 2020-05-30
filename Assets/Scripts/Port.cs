using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace h8s
{ 
    public class Port : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        public string Id { get; private set; }
        public string Name { get { return nameField.text; } private set { nameField.text = value; } }
        public PortDirection Direction { get; private set; }
        public DataType Type { get; private set; }
        public Node ParentNode { get; private set; }
        public Edge ConnectedEdge { get; private set; }
        
        private bool isInitialized = false;

        public void Initialize(Node parent, PortDirection direction, DataType type, string name, string id)
        {
            if (isInitialized) throw new UnityException(string.Format(
                "Attempt to initialize already initialized Port: {0}@{1}", 
                Name, ParentNode.name));

            ParentNode = parent;
            Direction = direction;
            Type = type;
            iconField.color = Utils.GetDataTypeColor(type);
            Name = name;
            Id = id;
            this.name = id;

            isInitialized = true;
        }

        public void Bind(Edge edge)
        {
            Unbind();
            ConnectedEdge = edge;
        }

        public void Unbind()
        {
            if (ConnectedEdge)
            {
                // Notify SchemeManager about this destroy
                Destroy(ConnectedEdge.gameObject);
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerEventData.InputButton.Right == eventData.button)
            {
                Unbind();
            }
        }
    }
}
