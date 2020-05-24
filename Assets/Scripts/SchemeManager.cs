using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using h8s.definitions;
using h8s.objects;

public class SchemeManager : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject nodePrefab;

    [Header("Automoton Icons reference")]
    [SerializeField] private Sprite terraformIcon;
    [SerializeField] private Sprite ansibleIcon;

    public static Canvas GUICanvas;

    private RectTransform canvasRt;

    private Node spawningNode;
    private NodeType spawningNodeType;
    private RectTransform spawningNodeRt;

    private void Awake()
    {
        canvasRt = GetComponent<RectTransform>();
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

        if (PointerEventData.InputButton.Left == eventData.button) SpawnNode(); else DiscartNode();
    }

    public void StartNodeSpawning(int nodeType)
    {
        var go = Instantiate(nodePrefab, transform) as GameObject;
        spawningNode = go.GetComponent<Node>();
        spawningNodeRt = go.GetComponent<RectTransform>();

        spawningNode.TurnGhost();
        spawningNodeType = (NodeType)nodeType;
    }

    private void DiscartNode()
    {
        Destroy(spawningNode);
        spawningNode = null;
        spawningNodeRt = null;
    }

    private void SpawnNode()
    {
        InitializeNode();

        spawningNode = null;
        spawningNodeRt = null;
    }

    private void UpdateNodePosition(float xOffset = 0, float yOffset = 0)
    {
        var new_position = Utils.ScreenToCanvasPosition(
            new Vector2(Input.mousePosition.x + xOffset, 
            Input.mousePosition.y + yOffset), canvasRt);

        spawningNodeRt.anchoredPosition = new_position;
    }

    private void InitializeNode()
    {
        var node = spawningNode.GetComponent<Node>();
        node.TurnSpawned();

        // This values should be set based on received schema from API
        var i = Random.Range(0, 1f);
        var icon = i > 0.5f ? terraformIcon : ansibleIcon;
        node.SetAutomotonIcon(icon);

        switch (spawningNodeType)
        {
            case NodeType.CreateVPC:
                node.SetName("Create VPC");

                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.Exec, "In");
                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.String, "Name");
                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.String, "CIDR");

                node.InstantiatePort(PortConst.Direction.Egress, PortConst.Type.Exec, "Out");
                node.InstantiatePort(PortConst.Direction.Egress, PortConst.Type.Object, "VPC");
                break;

            case NodeType.CreateSubnet:
                node.SetName("Create Subnet");

                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.Exec, "In");
                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.String, "Name");
                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.String, "CIDR");
                node.InstantiatePort(PortConst.Direction.Ingress, PortConst.Type.Object, "VPC");

                node.InstantiatePort(PortConst.Direction.Egress, PortConst.Type.Exec, "Out");
                node.InstantiatePort(PortConst.Direction.Egress, PortConst.Type.Object, "Subnet");
                break;
        }

    }
}
