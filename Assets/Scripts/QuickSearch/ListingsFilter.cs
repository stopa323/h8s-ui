using UnityEngine;
using TMPro;

namespace h8s
{
    public class ListingsFilter : MonoBehaviour
    {
        private TMP_InputField searchField;

        private void Awake()
        {
            searchField = GetComponent<TMP_InputField>();
        }

        public void OnValueChanged()
        {
            QuickSearchManager.Instance.ChangeListingsCriteria(searchField.text);
        }
    }
}
