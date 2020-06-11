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
    }

    [Serializable]
    public class NodeTemplate
    {
        public string kind;
        public string name;
        public string description;
        public string automoton;
        public List<string> keywords;
        public List<PortTemplate> ingressPorts;
        public List<PortTemplate> egressPorts;
    }

    [Serializable]
    public class NodeTemplateContainer
    {
        public List<NodeTemplate> items;
    }
}
