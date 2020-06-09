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
        [SerializeField] private Sprite coreIcon;

        private api.NodeTemplate tmpl;

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

        public void Initialize(api.NodeTemplate tmpl, Vector2 position)
        {
            this.tmpl = tmpl;
            this.tmpl.id = "Some_ID";
            name = this.tmpl.id;

            nameField.text = tmpl.name;

            automotonIcon.sprite = GetAutomotonIcon(tmpl.GetAutomoton());

            rt.anchoredPosition = position;
        }

        public void TurnGhost() { canvasGroup.alpha = 0.6f; }

        public void TurnSpawned() { canvasGroup.alpha = 1.0f; }

        public void InstantiatePort(PortDirection direction, api.PortTemplate portTmpl)
        {
            switch (direction)
            {
                case PortDirection.Ingress:
                    var inPort = InstantiateIngressPort();
                    inPort.Initialize(
                        this, 
                        direction,
                        portTmpl);
                    break;
                case PortDirection.Egress:
                    var outPort = InstantiateEgressPort();
                    outPort.Initialize(
                        this, 
                        direction, portTmpl);
                    break;
            }
            RefreshSize();
        }

        private IngressPort InstantiateIngressPort()
        {
            var portObj = Instantiate(ingressPortPrefab, ingressContainer.transform);
            var port = portObj.GetComponent<IngressPort>();
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
                    return coreIcon;
            }
        }

        public void OnNodeMove(Vector2 shift)
        {
            foreach(var p in ingressPorts)
            {
                p.OnParentMove(shift);
            }
            foreach (var p in egressPorts)
            {
                p.OnParentMove(shift);
            }
        }
    }

}
