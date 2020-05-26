using UnityEngine;

namespace h8s
{
    public class Edge : MonoBehaviour
    {
        private EdgeDrawer drawer;

        private void Awake()
        {
            drawer = GetComponent<EdgeDrawer>();
        }

        public void Initialize(Vector2 initPosition, DataType portType)
        {
            drawer.Initialize(initPosition, portType);
        }

        public void MoveEndAnchor(Vector2 newPosition)
        {
            drawer.MoveEndAnchor(newPosition);
        }
    }

}