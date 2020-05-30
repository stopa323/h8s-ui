using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace h8s
{ 
    public class PortBase : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        [Header("Icon References")]
        [SerializeField] private Sprite portEmptyIcon;
        [SerializeField] private Sprite portFilledIcon;

        public string Id { get; private set; }
        public string Name { get { return nameField.text; } private set { nameField.text = value; } }
        public PortDirection Direction { get; private set; }
        public DataType Type { get; private set; }
        public Node ParentNode { get; private set; }
        public Edge ConnectedEdge { get; private set; }
        
        private bool isInitialized = false;

        private void Awake()
        {
            iconField.sprite = portEmptyIcon;
        }

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

        public void FillIcon()
        {
            iconField.sprite = portFilledIcon;
        }

        public void EmptyIcon()
        {
            iconField.sprite = portEmptyIcon;
        }

        public void Bind(Edge edge)
        {
            if (ConnectedEdge)
            {
                Destroy(ConnectedEdge.gameObject);
            }
            ConnectedEdge = edge;
            FillIcon();
        }

        public void Unbind()
        {
            EmptyIcon();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (PointerEventData.InputButton.Right == eventData.button && ConnectedEdge)
            {
                Destroy(ConnectedEdge.gameObject);
            }
        }
    }
}
