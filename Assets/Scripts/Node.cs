using UnityEngine;
using UnityEngine.UI;

using h8s.definitions;
using h8s.objects;
using System.Collections.Generic;


namespace h8s.objects
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

        public void InstantiatePort(PortConst.Direction direction, PortConst.Type type, string name)
        {
            Port port = null;
            switch (direction)
            {
                case PortConst.Direction.Ingress:
                    port = InstantiateIngressPort(type, name);
                    break;
                case PortConst.Direction.Egress:
                    port = InstantiateEgressPort(type, name);
                    break;
            }

            port.PortType = type;
            port.PortName = name;
            
            RefreshSize();
        }

        private Port InstantiateIngressPort(PortConst.Type type, string name)
        {
            var portObj = Instantiate(ingressPortPrefab, ingressContainer.transform);
            var port = portObj.GetComponent<Port>();

            ingressPorts.Add(port);

            return port;
        }

        private Port InstantiateEgressPort(PortConst.Type type, string name)
        {
            var portObj = Instantiate(egressPortPrefab, egressContainer.transform);
            var port = portObj.GetComponent<Port>();

            egressPorts.Add(port);

            return port;
        }

        private void RefreshSize()
        {
            var margin = 10f;
            var portContainerHeight = Mathf.Max(ingressPorts.Count, egressPorts.Count) * PortConst.HEIGHT;
            rt.sizeDelta = new Vector2(rt.rect.width, NodeConst.HEADER_HEIGHT + portContainerHeight + margin);
        }
    }

}
