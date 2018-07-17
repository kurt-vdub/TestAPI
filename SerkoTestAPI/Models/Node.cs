using System.Collections.Generic;
using System.Linq;

namespace SerkoTestAPI.Models
{
    public class Node
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public List<Node> Children { get; } = new List<Node>();

        public Node GetNodeByName(string nodeName)
        {
            if (Name == nodeName) return this;

            foreach (var child in Children)
            {
                Node nodeByName = child.GetNodeByName(nodeName);
                if (nodeByName != null) return nodeByName;
            }

            return null;
        }

        public bool ShouldSerializeChildren()
        {
            return Children.Any();
        }
    }
}