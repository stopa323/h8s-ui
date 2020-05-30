using UnityEngine;

namespace h8s
{ 
    public class IngressPort : PortBase
    {
        [SerializeField] private TMPro.TMP_InputField inputField;

        private void Awake()
        {
            inputField.gameObject.SetActive(false);
        }

        public void Initialize(Node parent, PortDirection direction, DataType type, string name, string id,
            bool hasValue, string value)
        {
            Initialize(parent, direction, type, name, id);

            if (hasValue)
            {
                inputField.gameObject.SetActive(true);
                inputField.text = value;
            }
        }
    }
}
