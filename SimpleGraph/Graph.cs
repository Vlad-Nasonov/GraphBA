using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleGraph
{
    public class Graph
    {
        public List<Node> Nodes { get; set; }
        public void Add(int value, int x, int y) => Nodes.Add(new Node(value,x,y));
        public void Remove(Node node)
        {
            node.RemoveNodes();
            Nodes.Remove(node);
        }
        public void ClearWas()
        {
            foreach (var item in Nodes)
            {
                item.was = false;
            }
        }
        public Graph()
        {
            Nodes = new List<Node>();
        }
        public List<Node> GetPointOnWay(Node A, Node B)
        {
            Ways = new List<List<Node>>();
            CurnetWay = new List<Node>();
            ClearWas();
            GetWays(A, B);
            if (Ways.Count == 0)
                return null;
            List<Node> nodes = new List<Node>();
            for (int i = 1; i < Ways[0].Count-1; i++)
            {
                bool t = true;
                for (int j = 1; j < Ways.Count; j++)
                {
                    if(!Ways[j].Contains(Ways[0][i]))
                    {
                        t = false;
                        break;
                    }
                }
                if (t)
                    nodes.Add( Ways[0][i]);
            }
            if (nodes.Count == 0)
                return null;
            return nodes;
        }
        List<List<Node>> Ways;
        List<Node> CurnetWay;
        private void GetWays(Node A, Node B)
        {
            A.was = true;
            CurnetWay.Add(A);
            if (A == B)
            {
                Ways.Add(new List<Node>(CurnetWay));
                CurnetWay.Remove(A);
                A.was = false;
                return;
            }
            foreach (var item in A.Nodes)
            {
                if (!item.was)
                    GetWays(item, B);
            }
            CurnetWay.Remove(A);
            A.was = false;
        }
    }
}
