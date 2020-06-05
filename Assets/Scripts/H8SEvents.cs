using UnityEngine;
using UnityEngine.Events;


namespace h8s
{
    public class NodeTemplateEvent : UnityEvent<api.NodeTemplate> { }

    public class H8SEvents : MonoBehaviour
    {
        public static H8SEvents Instance { get; private set; }

        public NodeTemplateEvent NodeSpawnBegin = new NodeTemplateEvent();

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
        }
    }
}
