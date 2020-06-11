using UnityEngine;
using UnityEngine.EventSystems;

namespace h8s
{
    public enum BlackboadFocus { Sheet, QuickSearch }

    public class BlackboardManager : MonoBehaviour, IPointerClickHandler
    {
        public static BlackboardManager Instance { get; private set; }

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
            H8SEvents.Instance.E_NodeSpawned.AddListener(OnNodeSpawned);
        }

        #region Event handlers
        /* Handle creating node from QuickSearch */
        public void OnNodeSpawned(api.NodeTemplate template)
        {
            ToggleOffQuickSearch();
            CreateNodeFromTemplate(template);
        }
        #endregion

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

        /* Creates new Node element from selected template */
        public void CreateNodeFromTemplate(api.NodeTemplate template)
        {
            var node_obj = Instantiate(nodePrefab, transform) as GameObject;
            var node = node_obj.GetComponent<Node>();

            node.InitializeFromTemplate(
                template, 
                Utils.ScreenToCanvasPosition(Input.mousePosition));
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
