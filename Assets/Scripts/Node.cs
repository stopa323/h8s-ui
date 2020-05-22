using UnityEngine;
using UnityEngine.UI;

public class Node : MonoBehaviour
{
    [Header("GUI Elements reference")]
    [SerializeField] private Image automotonIcon;
    [SerializeField] private TMPro.TextMeshProUGUI name;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void TurnGhost() { canvasGroup.alpha = 0.6f; }

    public void TurnSpawned() { canvasGroup.alpha = 1.0f; }

    public void SetAutomotonIcon(Sprite image) { automotonIcon.sprite = image; }

    public void SetName(string newName)
    {
        name.text = newName;
    }
}
