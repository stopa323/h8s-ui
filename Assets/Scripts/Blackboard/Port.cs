using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace h8s
{ 
    public class Port : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;
        [SerializeField] private TMPro.TMP_InputField valueField;

        [Header("Icon References")]
        [SerializeField] private Sprite portEmptyIcon;
        [SerializeField] private Sprite portFilledIcon;

        #region Public properties
        public string Id { get; private set; }
        public string Name { get { return nameField.text; } private set { nameField.text = value; } }
        public DataType Type { get; private set; }
        public PortDirection Direction { get; private set; }
        public string Value { get { return valueField.text; } private set { valueField.text = value; } }

        public Node ParentNode { get; private set; }
        public Edge ConnectedEdge { get; private set; }
        #endregion

        private void Awake()
        {
            iconField.sprite = portEmptyIcon;
        }

        /* Initialize port's properties from loaded template */
        public void InitializeFromTemplate(Node parent, PortDirection direction, api.PortTemplate template)
        {
            ParentNode = parent;
            Direction = direction;

            // Note: This Id should be obtained from backend on save
            Id = Guid.NewGuid().ToString();
            name = Id;

            Name = template.name;

            // Few steps to set Type
            var type = Utils.DataTypeFromString(template.kind);
            Type = type;
            iconField.color = Utils.GetDataTypeColor(type);

            if (valueField && null == template.defaultValue)
            {
                valueField.enabled = false;
            }
            else if (valueField)
            {
                valueField.enabled = true;
                Value = template.defaultValue;
            }
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
