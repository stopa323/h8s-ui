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

        public void Initialize(Node parent, PortDirection direction, api.PortTemplate tmpl)
        {
            base.Initialize(parent, direction, tmpl);

            if (tmpl.defaultValue != null)
            {
                inputField.gameObject.SetActive(true);
                inputField.text = tmpl.defaultValue;
            }
        }
    }
}
