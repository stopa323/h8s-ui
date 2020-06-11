using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;

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

        #region Public properties
        public string Id { get; private set; }
        public string Name { get { return nameField.text; } private set { nameField.text = value; } }
        public string Description { get; private set; }
        public string Kind { get; private set; }
        public NodeAutomoton Automoton { get; private set; }

        public List<string> Keywords { get; private set; }
        public List<Port> IngressPorts { get; private set; }
        public List<Port> EgressPorts { get; private set; }
        #endregion

        private CanvasGroup canvasGroup;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        /* Initializes node's properties from loaded template */
        public void InitializeFromTemplate(api.NodeTemplate template, Vector2 position)
        {
            // Note: This Id should be obtained from backend on save
            Id = Guid.NewGuid().ToString();
            name = Id;

            Name = template.name;
            Description = template.description;
            Kind = template.kind;

            // Few steps to set Automoton
            var _automoton = Utils.AutomotonFromString(template.automoton);
            Automoton = _automoton;
            automotonIcon.sprite = GetAutomotonIcon(_automoton);

            // Ports initialization
            IngressPorts = new List<Port>();
            EgressPorts = new List<Port>();
            foreach (var iport in template.ingressPorts) {
                CreateIngressPortFromTemplate(iport);
            }
            foreach (var eport in template.egressPorts)
            {
                CreateEgressPortFromTemplate(eport);
            }

            // Set initial node element position
            var rt = GetComponent<RectTransform>();
            rt.anchoredPosition = position;
        }

        private void CreateIngressPortFromTemplate(api.PortTemplate template)
        {
            var port_obj = Instantiate(ingressPortPrefab, ingressContainer.transform);
            var port = port_obj.GetComponent<Port>();

            port.InitializeFromTemplate(this, PortDirection.Ingress, template);

            IngressPorts.Add(port);
        }

        private void CreateEgressPortFromTemplate(api.PortTemplate template)
        {
            var port_obj = Instantiate(egressPortPrefab, egressContainer.transform);
            var port = port_obj.GetComponent<Port>();

            port.InitializeFromTemplate(this, PortDirection.Egress, template);

            EgressPorts.Add(port);
        }

        public void TurnGhost() { canvasGroup.alpha = 0.6f; }

        public void TurnSpawned() { canvasGroup.alpha = 1.0f; }

        private Sprite GetAutomotonIcon(NodeAutomoton automoton)
        {
            switch (automoton)
            {
                case NodeAutomoton.Ansible:
                    return ansibleIcon;
                case NodeAutomoton.Terraform:
                    return terraformIcon;
                case NodeAutomoton._Core:
                    return coreIcon;
                default:
                    throw new UnityException(string.Format("Unknown automoton received: {0}", automoton));
            }
        }

        public void OnNodeMove(Vector2 shift)
        {
            foreach(var p in IngressPorts)
            {
                p.OnParentMove(shift);
            }
            foreach (var p in EgressPorts)
            {
                p.OnParentMove(shift);
            }
        }

        public void DestroyNode()
        {
            Destroy(gameObject);
        }
    }

}
