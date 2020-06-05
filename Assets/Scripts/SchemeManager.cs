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

        private void Start()
        {
            H8SEvents.Instance.NodeSpawnBegin.AddListener(E_OnNodeSpawnBegin);
        }

        public void E_OnNodeSpawnBegin(api.NodeTemplate tmpl)
        {
            ToggleOffQuickSearch();
            InstantiateNode(tmpl);
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
        }

        public void LoadNodesAsync()
        {
            StartCoroutine(api.Client.Instance.LoadNodes());
        }

        public Node InstantiateNode(api.NodeTemplate nodeDefinition)
        {
            var node_obj = Instantiate(nodePrefab, transform) as GameObject;
            var node = node_obj.GetComponent<Node>();

            node.Initialize(nodeDefinition, 
                Utils.ScreenToCanvasPosition(Input.mousePosition));

            //foreach(var port in nodeDefinition.ingressPorts)
            //{
            //    node.InstantiatePort(PortDirection.Ingress, port);
            //}

            //foreach (var port in nodeDefinition.egressPorts)
            //{
            //    node.InstantiatePort(PortDirection.Egress, port);
            //}
            return node;
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
