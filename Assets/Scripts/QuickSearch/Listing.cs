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
            H8SEvents.Instance.NodeSpawnBegin.Invoke(nodeTemplate);
        }
    }
}
