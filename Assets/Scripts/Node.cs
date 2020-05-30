using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace h8s
{
    public class Node : MonoBehaviour
    {
        [Header("GUI Elements reference")]
        [SerializeField] private Image automotonIcon;
        [SerializeField] private TMPro.TextMeshProUGUI nameField;

        [SerializeField] private GameObject ingressContainer;
        [SerializeField] private GameObject egressContainer;

        [Header("Prefabs")]
        [SerializeField] private GameObject ingressPortPrefab;
        [SerializeField] private GameObject egressPortPrefab;

        [Header("Available Automoton Icons")]
        [SerializeField] private Sprite terraformIcon;
        [SerializeField] private Sprite ansibleIcon;

        public string Id { get; private set; }
        public string Name { get { return nameField.text; } set { nameField.text = value; } }
        public NodeAutomoton Automoton { get; private set; }

        private CanvasGroup canvasGroup;
        private RectTransform rt;

        private List<PortBase> ingressPorts = new List<PortBase>();
        private List<PortBase> egressPorts = new List<PortBase>();

        private const float PORT_HEIGHT = 30f;
        private const float HEADER_HEIGHT = 40f;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rt = GetComponent<RectTransform>();
        }

        public void Initialize(string id, string name, NodeAutomoton automoton, Vector2 position)
        {
            Id = id;
            this.name = id;
            Name = name;
            Automoton = automoton;
            automotonIcon.sprite = GetAutomotonIcon(automoton);

            rt.anchoredPosition = position;
        }

        public void TurnGhost() { canvasGroup.alpha = 0.6f; }

        public void TurnSpawned() { canvasGroup.alpha = 1.0f; }

        public void InstantiatePort(PortDirection direction, DataType type, string name, string id)
        {
            PortBase port = null;
            switch (direction)
            {
                case PortDirection.Ingress:
                    port = InstantiateIngressPort();
                    break;
                case PortDirection.Egress:
                    port = InstantiateEgressPort();
                    break;
            }
            port.Initialize(this, direction, type, name, id);

            RefreshSize();
        }

        private PortBase InstantiateIngressPort()
        {
            var portObj = Instantiate(ingressPortPrefab, ingressContainer.transform);
            var port = portObj.GetComponent<PortBase>();
            ingressPorts.Add(port);

            return port;
        }

        private PortBase InstantiateEgressPort()
        {
            var portObj = Instantiate(egressPortPrefab, egressContainer.transform);
            var port = portObj.GetComponent<PortBase>();
            egressPorts.Add(port);

            return port;
        }

        private void RefreshSize()
        {
            var margin = 10f;
            var portContainerHeight = Mathf.Max(ingressPorts.Count, egressPorts.Count) * PORT_HEIGHT;
            rt.sizeDelta = new Vector2(rt.rect.width, HEADER_HEIGHT + portContainerHeight + margin);
        }

        private Sprite GetAutomotonIcon(NodeAutomoton automoton)
        {
            switch (automoton)
            {
                case NodeAutomoton.Ansible:
                    return ansibleIcon;
                case NodeAutomoton.Terraform:
                    return terraformIcon;
                default:
                    return terraformIcon;
            }
        }
    }

}
