using System;
using System.Collections.Generic;
using UnityEngine;

namespace h8s.api
{
    [Serializable]
    public class PortTemplate
    {
        public string name;
        public string kind;
        public string defaultValue;

        public DataType GetKind()
        {
            switch (kind)
            {
                case "EXEC":
                    return DataType.Exec;
                case "OBJECT":
                    return DataType.Object;
                case "STRING":
                    return DataType.String;
                case "BOOL":
                    return DataType.Bool;
                default:
                    Debug.LogWarningFormat("Unknown kind received: {0}", kind);
                    return DataType.Bool;
            }
        }
    }

    [Serializable]
    public class NodeTemplate
    {
        public string id;
        public string kind;
        public string name;
        public string description;
        public string automoton;
        public List<string> keywords;
        public List<PortTemplate> ingressPorts;
        public List<PortTemplate> egressPorts;

        public NodeAutomoton GetAutomoton()
        {
            switch (automoton)
            {
                case "ANSIBLE":
                    return NodeAutomoton.Ansible;
                case "_CORE":
                    return NodeAutomoton._Core;
                case "TERRAFORM":
                    return NodeAutomoton.Terraform;
                default:
                    Debug.LogWarningFormat("Unknown automoton received: {0}", automoton);
                    return NodeAutomoton._Core;
            }
        }
    }

    [Serializable]
    public class NodeTemplateContainer
    {
        public List<NodeTemplate> nodes;
    }
}
