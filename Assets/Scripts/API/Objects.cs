using System;
using System.Collections.Generic;
using UnityEngine;

namespace h8s.api
{
    [Serializable]
    public class Port
    {
        public string id;
        public string name;
        public string type;
        public bool hasValue;
        public string value;

        public DataType GetType()
        {
            switch (type)
            {
                case "EXEC":
                    return DataType.Exec;
                case "STRING":
                    return DataType.String;
                case "OBJECT":
                    return DataType.Object;
                case "BOOL":
                    return DataType.Bool;
                default:
                    Debug.LogWarningFormat("Unknown Port Type: {0}", type);
                    return DataType.Object;
            }
        }
    }

    [Serializable]
    public class NodePosition
    {
        public int x;
        public int y;
    }

    [Serializable]
    public class Node
    {
        public string id;
        public string name;
        public string automoton;
        public NodePosition position;

        public List<Port> ingressPorts;
        public List<Port> egressPorts;

        public NodeAutomoton GetAutomoton()
        {
            switch (automoton)
            {
                case "ANSIBLE":
                    return NodeAutomoton.Ansible;
                case "TERRAFORM":
                    return NodeAutomoton.Terraform;
                default:
                    Debug.LogWarningFormat("Unknown automoton: {0}", automoton);
                    return NodeAutomoton.Terraform;
            }
        }
        public Vector2 GetPosition()
        {
            return new Vector2(position.x, position.y);
        }
    }

    [Serializable]
    public class NodeContainer
    {
        public List<Node> nodes;
    }
}
