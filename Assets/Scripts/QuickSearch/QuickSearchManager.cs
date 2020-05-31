using UnityEngine;

namespace h8s
{
    public class QuickSearchManager : MonoBehaviour
    {
        [SerializeField] private GameObject categoryPrefab;
        [SerializeField] private Transform categoryContainer;

        private void Awake()
        {
            // Start coroutine here

            InstantiateCategory("Ansible");
            InstantiateCategory("System");
            InstantiateCategory("Terraform");
        }

        private void InstantiateCategory(string name)
        {
            var obj = Instantiate(categoryPrefab, categoryContainer);
            var category = obj.GetComponent<CategoryManager>();

            category.SetName(name);
            category.AddListing("Node Type 1");
            category.AddListing("Node Type 2");
            category.AddListing("Node Type 3");
        }
    }

}
