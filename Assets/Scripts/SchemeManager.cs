using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public enum BlackboadFocus { Sheet, QuickSearch }

    public class SchemeManager : MonoBehaviour, IPointerClickHandler
    {
        public static SchemeManager Instance { get; private set; }

        [SerializeField] private GameObject nodePrefab;
        [SerializeField] private QuickSearchManager quickSearch;

        public static Canvas GUICanvas { get; private set; }
        public static RectTransform GUICanvasRt { get; private set; }

        private Node spawningNode;
        private NodeKind spawningNodeType;
        private RectTransform spawningNodeRt;

        private BlackboadFocus blackboadFocus = BlackboadFocus.Sheet;

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
            switch(eventData.button)
            {
                case PointerEventData.InputButton.Right:
                    switch (blackboadFocus)
                    {
                        case BlackboadFocus.QuickSearch:
                        case BlackboadFocus.Sheet:
                            ToggleOnQuickSearch(
                                Utils.ScreenToCanvasPosition(eventData.pressPosition));
                            break;
                    }
                    break;
                case PointerEventData.InputButton.Left:
                    switch (blackboadFocus)
                    {
                        case BlackboadFocus.QuickSearch:
                            ToggleOffQuickSearch();
                            break;
                    }
                    break;
                case PointerEventData.InputButton.Middle:
                    break;
                default:
                    break;
            }
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

        private void ToggleOnQuickSearch(Vector2 position)
        {
            quickSearch.gameObject.SetActive(true);
            var rt = quickSearch.gameObject.GetComponent<RectTransform>();
            rt.anchoredPosition = position;
            blackboadFocus = BlackboadFocus.QuickSearch;
        }

        private void ToggleOffQuickSearch()
        {
            quickSearch.gameObject.SetActive(false);
            blackboadFocus = BlackboadFocus.Sheet;
        }
    }
}
