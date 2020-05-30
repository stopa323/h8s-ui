using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public class SchemeManager : MonoBehaviour, IPointerClickHandler
    {
        public static SchemeManager Instance { get; private set; }

        [SerializeField] private GameObject nodePrefab;

        public static Canvas GUICanvas { get; private set; }
        public static RectTransform GUICanvasRt { get; private set; }

        private Node spawningNode;
        private NodeKind spawningNodeType;
        private RectTransform spawningNodeRt;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }

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

            //if (PointerEventData.InputButton.Left == eventData.button) SpawnNode(); else DiscartNode();
        }

        public void StartNodeSpawning(int nodeType)
        {
            //var go = Instantiate(nodePrefab, transform) as GameObject;
            //spawningNode = go.GetComponent<Node>();
            //spawningNodeRt = go.GetComponent<RectTransform>();

            //spawningNode.TurnGhost();
            //spawningNodeType = (NodeKind)nodeType;
        }

        private void DiscartNode()
        {
            Destroy(spawningNode.gameObject);
            spawningNode = null;
            spawningNodeRt = null;
        }

        public void LoadNodesAsync()
        {
            StartCoroutine(api.Client.Instance.LoadNodes());
        }

        public void InstantiateNode(api.Node nodeDefinition)
        {
            var node_obj = Instantiate(nodePrefab, transform) as GameObject;
            var node = node_obj.GetComponent<Node>();

            node.Initialize(
                nodeDefinition.id, 
                nodeDefinition.name, 
                nodeDefinition.GetAutomoton(), 
                nodeDefinition.GetPosition());

            foreach(var port in nodeDefinition.ingressPorts)
            {
                node.InstantiatePort(PortDirection.Ingress, port);
            }

            foreach (var port in nodeDefinition.egressPorts)
            {
                node.InstantiatePort(PortDirection.Egress, port);
            }
        }

        private void UpdateNodePosition(float xOffset = 0, float yOffset = 0)
        {
            var new_position = Utils.ScreenToCanvasPosition(
                new Vector2(Input.mousePosition.x + xOffset,
                Input.mousePosition.y + yOffset));

            spawningNodeRt.anchoredPosition = new_position;
        }
    }
}
