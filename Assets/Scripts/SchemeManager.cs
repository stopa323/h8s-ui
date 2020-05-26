using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class SchemeManager : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private GameObject nodePrefab;

        [Header("Automoton Icons reference")]
        [SerializeField] private Sprite terraformIcon;
        [SerializeField] private Sprite ansibleIcon;

        public static Canvas GUICanvas { get; private set; }
        public static RectTransform GUICanvasRt { get; private set; }

        private Node spawningNode;
        private NodeKind spawningNodeType;
        private RectTransform spawningNodeRt;

        private void Awake()
        {
            GUICanvasRt = GetComponent<RectTransform>();
            GUICanvas = GetComponent<Canvas>();
        }

        private void Update()
        {
            if (!spawningNode) return;

            UpdateNodePosition(-30f, 30f);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (!spawningNode) return;

            if (PointerEventData.InputButton.Left == eventData.button) SpawnNode(); else DiscartNode();
        }

        public void StartNodeSpawning(int nodeType)
        {
            var go = Instantiate(nodePrefab, transform) as GameObject;
            spawningNode = go.GetComponent<Node>();
            spawningNodeRt = go.GetComponent<RectTransform>();

            spawningNode.TurnGhost();
            spawningNodeType = (NodeKind)nodeType;
        }

        private void DiscartNode()
        {
            Destroy(spawningNode.gameObject);
            spawningNode = null;
            spawningNodeRt = null;
        }

        private void SpawnNode()
        {
            InitializeNode();

            spawningNode = null;
            spawningNodeRt = null;
        }

        private void UpdateNodePosition(float xOffset = 0, float yOffset = 0)
        {
            var new_position = Utils.ScreenToCanvasPosition(
                new Vector2(Input.mousePosition.x + xOffset,
                Input.mousePosition.y + yOffset));

            spawningNodeRt.anchoredPosition = new_position;
        }

        private void InitializeNode()
        {
            var node = spawningNode.GetComponent<Node>();
            node.TurnSpawned();

            // This values should be set based on received schema from API
            var i = Random.Range(0, 1f);
            var icon = i > 0.5f ? terraformIcon : ansibleIcon;
            node.SetAutomotonIcon(icon);

            switch (spawningNodeType)
            {
                case NodeKind.CreateVPC:
                    node.SetName("Create VPC");

                    node.InstantiatePort(PortDirection.Ingress, DataType.Exec, "In");
                    node.InstantiatePort(PortDirection.Ingress, DataType.String, "Name");
                    node.InstantiatePort(PortDirection.Ingress, DataType.String, "CIDR");

                    node.InstantiatePort(PortDirection.Egress, DataType.Exec, "Out");
                    node.InstantiatePort(PortDirection.Egress, DataType.Object, "VPC");
                    break;

                case NodeKind.CreateSubnet:
                    node.SetName("Create Subnet");

                    node.InstantiatePort(PortDirection.Ingress, DataType.Exec, "In");
                    node.InstantiatePort(PortDirection.Ingress, DataType.String, "Name");
                    node.InstantiatePort(PortDirection.Ingress, DataType.String, "CIDR");
                    node.InstantiatePort(PortDirection.Ingress, DataType.Object, "VPC");

                    node.InstantiatePort(PortDirection.Egress, DataType.Exec, "Out");
                    node.InstantiatePort(PortDirection.Egress, DataType.Object, "Subnet");
                    break;
            }
        }
    }
}
