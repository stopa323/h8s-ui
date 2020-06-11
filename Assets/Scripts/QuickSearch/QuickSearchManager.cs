using System.Collections.Generic;
using UnityEngine;

namespace h8s
{
    public class QuickSearchManager : MonoBehaviour
    {
        public static QuickSearchManager Instance { get; private set; }

        [SerializeField] private GameObject categoryPrefab;
        [SerializeField] private Transform categoryContainer;

        private Dictionary<NodeAutomoton, CategoryManager> nodeTemplates = new Dictionary<NodeAutomoton, CategoryManager>();

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

            StartCoroutine(api.Client.Instance.FetchNodeDefinitions());
        }

        public void AddNodeTemplate(api.NodeTemplate node)
        {
            var automoton = Utils.AutomotonFromString(node.automoton);

            /* If autmoton is new, spawn category */
            if (!nodeTemplates.ContainsKey(automoton))
            {
                var obj = Instantiate(categoryPrefab, categoryContainer);
                var cm = obj.GetComponent<CategoryManager>();
                cm.SetName(automoton.ToString());
                nodeTemplates.Add(automoton, cm);
            }

            var categoryManager = nodeTemplates[automoton];
            categoryManager.AddListing(node);
        }

        public void ChangeListingsCriteria(string keyword)
        {
            foreach(KeyValuePair<NodeAutomoton, CategoryManager> entry in nodeTemplates)
            {
                var valid_listings = entry.Value.SetListingKeywordFilter(keyword);
                if (0 == valid_listings)
                {
                    entry.Value.gameObject.SetActive(false);
                }
                else
                {
                    entry.Value.gameObject.SetActive(true);
                }
            }
        }
    }

}
