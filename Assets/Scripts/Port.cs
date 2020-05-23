using UnityEngine;
using UnityEngine.UI;
using h8s.definitions;

namespace h8s.objects
{ 
    public class Port : MonoBehaviour
    {
        [SerializeField] private Image iconField;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        private Node node;
        private PortConst.Type _port_type;

        public string PortName { get { return nameField.text; } set { nameField.text = value; } }

        public PortConst.Type PortType {
            get { return _port_type; }
            set {
                _port_type = value;
                iconField.color = PortConst.ColorMap[value];
            }
        }
    }
}
