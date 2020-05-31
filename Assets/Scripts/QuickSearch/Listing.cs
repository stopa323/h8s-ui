using UnityEngine;
using TMPro;

public class Listing : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textField;

    public void SetName(string name)
    {
        textField.text = name;
    }
}
