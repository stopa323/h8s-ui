using UnityEngine;
using TMPro;

namespace h8s
{
    public class Listing : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;

        private api.NodeTemplate nodeTemplate;

        public void Initialize(api.NodeTemplate nodeTemplate)
        {
            textField.text = nodeTemplate.name;
            this.nodeTemplate = nodeTemplate;
        }

        public void SpawnNode()
        {
            H8SEvents.Instance.E_NodeSpawned.Invoke(nodeTemplate);
        }

        public int GetKeyworkMatchFactor(string keyword)
        {
            if ("" == keyword) return 1;

            if (nodeTemplate.keywords.Contains(keyword)) return 1;

            return 0;
        }
    }
}
