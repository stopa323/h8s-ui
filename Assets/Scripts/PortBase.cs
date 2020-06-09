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

        private api.PortTemplate tmpl;

        public string Id { get; }
        public string Name { get { return tmpl.name; } }
        public PortDirection Direction { get; private set; }
        public DataType Type { get { return tmpl.GetKind(); } }
        public Node ParentNode { get; private set; }
        public Edge ConnectedEdge { get; private set; }
        
        private void Awake()
        {
            iconField.sprite = portEmptyIcon;
        }

        public void Initialize(Node parent, PortDirection direction, api.PortTemplate tmpl)
        {
            ParentNode = parent;
            Direction = direction;
            this.tmpl = tmpl;

            iconField.color = Utils.GetDataTypeColor(Type);
            nameField.text = tmpl.name;
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

        public void OnParentMove(Vector2 shift)
        {
            if (null == ConnectedEdge) { return; }

            ConnectedEdge.OnPortMoved(this, shift);
        }

        private void OnDestroy()
        {
            if (ConnectedEdge) { Destroy(ConnectedEdge.gameObject); }
        }
    }
}
