using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SimpleGraph;

namespace Drawing
{
    public class DrawingGraph
    {
        Size size;
        public Graph graph { get; set; }
        Bitmap bitmap;
        Graphics G;
        int R = 20;
        public DrawingGraph(Size size, Graph graph = null)
        {
            if (graph == null)
                this.graph = new Graph();
            else
                this.graph = graph;
            this.size = size;
        }
        private double DistTo(Point P, Node node) => Math.Sqrt(Math.Pow(P.X - node.X, 2) + Math.Pow(P.Y - node.Y, 2));
        public Node GetNode(Point p)
        {
            foreach (var item in graph.Nodes)
            {
                if (DistTo(p, item) < R)
                    return item;
            }
            return null;
        }
        public bool PlaceFree(Point p)
        {
            foreach (var item in graph.Nodes)
            {
                if (DistTo(p, item) < 4 * R)
                    return false;
            }
            return true;
        }
        public Bitmap Draw()
        {
            bitmap = new Bitmap(size.Width, size.Height);
            G = Graphics.FromImage(bitmap);
            graph.ClearWas();
            foreach (var item in graph.Nodes)
            {
                DrawNode(item);
            }
            return bitmap;
        }
        private void DrawNode(Node node)
        {
            if(!node.was)
            {
                node.was = true;
                foreach (var item in node.Nodes)
                {
                    if (!item.was)
                        G.DrawLine(Pens.Black, node.X, node.Y, item.X, item.Y);
                }
                G.FillEllipse(Brushes.LightCyan, node.X - R, node.Y - R, 2 * R, 2 * R);
                G.DrawEllipse(Pens.Cyan, node.X - R, node.Y - R, 2 * R, 2 * R);
                float t = 0;
                string text = node.Value.ToString();
                do
                {
                    t += (float)0.2;
                }
                while (G.MeasureString(text, new Font("Microsoft Sans Serif", t)).Width < R); StringFormat sf = new StringFormat
                {
                    Alignment = StringAlignment.Center,
                    LineAlignment = StringAlignment.Center
                };
                G.DrawString(text, new Font("Microsoft Sans Serif", t), Brushes.Black, new Rectangle(node.X - R, node.Y - R, 2 * R, 2 * R), sf);
            }
        }
    }
}
