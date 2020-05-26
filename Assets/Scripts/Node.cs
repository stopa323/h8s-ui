using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

namespace h8s
{
    public class Node : MonoBehaviour
    {
        [Header("GUI Elements reference")]
        [SerializeField] private Image automotonIcon;
        [SerializeField] private TMPro.TextMeshProUGUI name;

        [SerializeField] private GameObject ingressContainer;
        [SerializeField] private GameObject egressContainer;

        [Header("Prefabs")]
        [SerializeField] private GameObject ingressPortPrefab;
        [SerializeField] private GameObject egressPortPrefab;

        private CanvasGroup canvasGroup;
        private RectTransform rt;

        private List<Port> ingressPorts = new List<Port>();
        private List<Port> egressPorts = new List<Port>();

        private const float PORT_HEIGHT = 30f;
        private const float HEADER_HEIGHT = 40f;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
            rt = GetComponent<RectTransform>();
        }

        public void TurnGhost() { canvasGroup.alpha = 0.6f; }

        public void TurnSpawned() { canvasGroup.alpha = 1.0f; }

        public void SetAutomotonIcon(Sprite image) { automotonIcon.sprite = image; }

        public void SetName(string newName)
        {
            name.text = newName;

        }

        public void InstantiatePort(PortDirection direction, DataType type, string name)
        {
            Port port = null;
            switch (direction)
            {
                case PortDirection.Ingress:
                    port = InstantiateIngressPort(name);
                    break;
                case PortDirection.Egress:
                    port = InstantiateEgressPort(name);
                    break;
            }

            port.PortType = type;
            port.PortName = name;
            
            RefreshSize();
        }

        private Port InstantiateIngressPort(string name)
        {
            var portObj = Instantiate(ingressPortPrefab, ingressContainer.transform);
            var port = portObj.GetComponent<Port>();
            port.PortDirection = PortDirection.Ingress;

            ingressPorts.Add(port);

            return port;
        }

        private Port InstantiateEgressPort(string name)
        {
            var portObj = Instantiate(egressPortPrefab, egressContainer.transform);
            var port = portObj.GetComponent<Port>();
            port.PortDirection = PortDirection.Egress;

            egressPorts.Add(port);

            return port;
        }

        private void RefreshSize()
        {
            var margin = 10f;
            var portContainerHeight = Mathf.Max(ingressPorts.Count, egressPorts.Count) * PORT_HEIGHT;
            rt.sizeDelta = new Vector2(rt.rect.width, HEADER_HEIGHT + portContainerHeight + margin);
        }
    }

}
