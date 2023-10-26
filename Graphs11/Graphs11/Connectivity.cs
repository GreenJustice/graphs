using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    using Vertex = System.Int32;
    public static class Connectivity
    {
        public static List<HashSet<Vertex>> IsConnectNotOriented(this IGraph graph)
        {
            bool[] visitedVertex = new bool[graph.VertexCount()];
            var connectedComponent = new List<HashSet<Vertex>>();
            connectedComponent.Add(new HashSet<Vertex>());
            var start = 0;
            var queue = new Queue<Vertex>();
            int i = 0;
            //visitedVertex[i] = true;

            var newgraph = GetOriented(graph);
            queue.Enqueue(start);
            while (visitedVertex.Any(v => v == false))
            {
                while (queue.Count() != 0)
                {
                    var current = queue.Dequeue();
                    connectedComponent[i].Add(current);
                    visitedVertex[current] = true;
                    foreach (var next in newgraph.AdjacencyList(current))
                    {
                        if (!connectedComponent[i].Contains(next))
                        {
                            queue.Enqueue(next);
                        }
                    }
                }
                for (int j = 0; j < graph.VertexCount(); j++)
                {
                    if (visitedVertex[j] == false)
                    {
                        i++;
                        connectedComponent.Add(new HashSet<Vertex>());
                        queue.Enqueue(j);
                        break;
                    }
                }
            }


            //for (i = 0; i < connectedComponent.Count(); i++)
            //{
            //    foreach (var next in connectedComponent[i])
            //    {
            //        Console.Write(next+ " ");
            //    }
            //    Console.WriteLine();
            //}
            
            return connectedComponent;
        }
        
        public static IGraph GetOriented( IGraph graph)
        {
            var orMatrix = graph.AdjacencyMatrix();
            for (int i = 0; i<graph.VertexCount();i++)
            {
                for (int j = 0; j < graph.VertexCount(); j++)
                {
                    if (orMatrix[i, j] != orMatrix[j, i] && orMatrix[j,i]!=0)
                    {
                        orMatrix[i,j] = orMatrix[j, i];
                    }
                    if (orMatrix[j, i] != orMatrix[i, j] && orMatrix[i, j] != 0)
                    {
                        orMatrix[j, i] = orMatrix[i, j];
                    }
                }
            }
            return new GraphMatrix(orMatrix);
        }

    }
}
