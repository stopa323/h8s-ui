using UnityEngine;

public class Edge : MonoBehaviour
{
    private EdgeDrawer drawer;

    private void Awake()
    {
        drawer = GetComponent<EdgeDrawer>();
    }

    public void Initialize(Vector2 initPosition)
    {
        drawer.Initialize(initPosition);
    }

    public void MoveEndAnchor(Vector2 newPosition)
    {
        drawer.MoveEndAnchor(newPosition);
    }
}
