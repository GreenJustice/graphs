using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    public static class BellmanFordAlgorithm
    {
       

        
        public static Tuple<int[], bool> BellmanFindShortestPath(this IGraph graph,int sourceVertex)
        {
        
            var edges = graph.ListOfEdges();
            var verticesCount = graph.VertexCount();
            int[] distances = new int[verticesCount];

            for (int i = 0; i < verticesCount; i++)
            {
                distances[i] = 9999999;
            }

            distances[sourceVertex] = 0;

            for (int i = 0; i < verticesCount - 1; i++)
            {
                foreach (var edge in edges)
                {
                    if (distances[edge.Item1] + edge.Item3 < distances[edge.Item2])
                    {
                        distances[edge.Item2] = distances[edge.Item1] + edge.Item3;
                    }
                }
                
            }
            var negative = edges.Any(e => distances[e.Item1] + e.Item3 < distances[e.Item2]);
            return Tuple.Create(distances, negative);
        }
    }
}
