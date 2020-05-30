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
    }
}
