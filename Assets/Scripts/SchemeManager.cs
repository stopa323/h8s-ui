using UnityEngine;
using UnityEngine.EventSystems;

public class SchemeManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject nodePrefab;

    private bool isSpawningNode = false;
    private RectTransform canvasRt;

    private GameObject spawningNode;
    private RectTransform spawningNodeRt;

    private void Awake()
    {
        canvasRt = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (!isSpawningNode) return;

        UpdateNodePosition(-30f, 30f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!isSpawningNode) return;

        if (PointerEventData.InputButton.Left == eventData.button) SpawnNode(); else DiscartNode();
    }

    public void StartNodeSpawning()
    {
        spawningNode = Instantiate(nodePrefab, transform);
        spawningNodeRt = spawningNode.GetComponent<RectTransform>();
        isSpawningNode = true;
    }

    private void DiscartNode()
    {
        Destroy(spawningNode);
        spawningNode = null;
        spawningNodeRt = null;
        isSpawningNode = false;
    }

    private void SpawnNode()
    {
        spawningNode = null;
        spawningNodeRt = null;
        isSpawningNode = false;
    }

    private void UpdateNodePosition(float xOffset = 0, float yOffset = 0)
    {
        var new_position = Utils.ScreenToCanvasPosition(
            new Vector2(Input.mousePosition.x + xOffset, 
            Input.mousePosition.y + yOffset), canvasRt);

        spawningNodeRt.anchoredPosition = new_position;
    }
}
