using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SimpleGraph
{
    public class FileGraph
    {
        public string Path { get; set; }
        public FileGraph(string path) => Path = path;
        public void SaveGraph(Graph graph)
        {
            FileStream file = new FileStream(Path, FileMode.Create);
            BinaryWriter writer = new BinaryWriter(file);
            writer.Write(graph.Nodes.Count);
            foreach (var item in graph.Nodes)
            {
                writer.Write(item.Value);
                writer.Write(item.X);
                writer.Write(item.Y);
            }
            for (int i = 0; i < graph.Nodes.Count-1; i++)
                for (int j = i+1; j < graph.Nodes.Count; j++)
                    writer.Write(graph.Nodes[i].Nodes.Contains(graph.Nodes[j]));
            writer.Close();
            file.Close();
        }
        public Graph OpenGraph()
        {
            FileStream file = new FileStream(Path, FileMode.Open);
            BinaryReader reader = new BinaryReader(file);
            Graph graph = new Graph();
            int N = reader.ReadInt32();
            for (int i = 0; i < N; i++)
            {
                int value = reader.ReadInt32();
                int X = reader.ReadInt32();
                int Y = reader.ReadInt32();
                graph.Add(value, X, Y);
            }
            for (int i = 0; i < N-1; i++)
                for (int j = i+1; j < N; j++)
                {
                    if (reader.ReadBoolean())
                        graph.Nodes[i].AddNode(graph.Nodes[j]);
                }
            return graph;
        }
    }
}
