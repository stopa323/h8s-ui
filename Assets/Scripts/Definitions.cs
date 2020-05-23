using System.Collections.Generic;
using UnityEngine;

namespace h8s.definitions
{
    public static class PortConst
    {
        public const float HEIGHT = 30f;

        public enum Direction { Ingress, Egress }
        public enum Type { Exec, Bool }

        public static Dictionary<Type, Color32> ColorMap = new Dictionary<Type, Color32>
        {
            { Type.Exec, new Color32(255, 255, 255, 255) },
            { Type.Bool, new Color32(207, 51, 13, 255) }
        };
    }

    public static class NodeConst
    {
        public const float HEADER_HEIGHT = 40f;
    }
}
