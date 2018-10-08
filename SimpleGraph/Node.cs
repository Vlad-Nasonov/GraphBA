using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGraph
{
    public class Node
    {
        public int Value { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public List<Node> Nodes { get; set; }
        public void AddNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();
            Nodes.Add(node);
            node.Nodes.Add(this);
        }
        public void RemoveNode(Node node)
        {
            if (node == null)
                throw new ArgumentNullException();
            Nodes.Remove(node);
            node.Nodes.Remove(this);
        }
        internal void RemoveNodes()
        {
            foreach (var item in Nodes)
            {
                item.Nodes.Remove(this);
            }
            Nodes.Clear();
        }
        public Node(int value, int x, int y)
        {
            Nodes = new List<Node>();
            Value = value;
            X = x;
            Y = y;
        }
        public bool was = false;
        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
