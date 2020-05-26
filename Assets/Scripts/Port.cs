using UnityEngine;
using UnityEngine.UI;

namespace h8s
{ 
    public class Port : MonoBehaviour
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        private Node node;
        private DataType _port_type;

        public string PortName { get { return nameField.text; } set { nameField.text = value; } }
        public PortDirection PortDirection { get; set; }

        public DataType PortType {
            get { return _port_type; }
            set {
                _port_type = value;
                iconField.color = Utils.GetDataTypeColor(value);
            }
        }
    }
}
