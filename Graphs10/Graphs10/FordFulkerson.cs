using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs10
{
    public static class FordFulkersonAlg
    {
        public static bool bfs(int[,] matrix, int s, int t, int[] parent, int vertexCount)
        {
   
            bool[] visited = new bool[vertexCount];
            for (int  i = 0; i<vertexCount;i++)
            {
                visited[i] = false; 
            }
            List<int> queue = new List<int>();
            queue.Add(s);
            visited[s] = true;
            parent[s] = -1;
            while (queue.Count > 0)
            {
                int u = queue[0];
                queue.RemoveAt(0);
                for (int vertex = 0; vertex < vertexCount ;vertex++ )
                {
                    if (visited[vertex] == false && matrix[u, vertex]> 0  ) 
                    {
                        if (vertex==t)
                        {
                            parent[vertex] = u;
                            return true; 
                        }
                        queue.Add(vertex);
                        parent[vertex] = u;
                        visited[vertex] = true; 
                    }
                }
            }
            return false; 
        }
        public static Tuple<int, int[,]> FordFulkerson(this IGraph graph, int s, int t)
        {
            
                var matrix = graph.AdjacencyMatrix();
                var vertexCount = graph.VertexCount();
                int u, v;
                int[,] rGraph = new int[vertexCount, vertexCount];
                for (u = 0; u < vertexCount; u++)
                {
                    for (v = 0; v < vertexCount; v++)
                    {
                        rGraph[u, v] = matrix[u, v];
                    }
                }
                int[] parent = new int[vertexCount];
                int maxFlow = 0;
                while (bfs(rGraph, s, t, parent, vertexCount))
                {
                    int pathFlow = int.MaxValue;
                    for (v = t; v != s; v = parent[v])
                    {
                        u = parent[v];
                        pathFlow = Math.Min(pathFlow, rGraph[u, v]);
                    }
                    for (v = t; v != s; v = parent[v])
                    {
                        u = parent[v];
                        rGraph[u, v] -= pathFlow;
                        rGraph[v, u] += pathFlow;
                    }
                    maxFlow += pathFlow;
                }
            //for (u = 0; u < vertexCount; u++)
            //{
            //    for (v = 0; v < vertexCount; v++)
            //    {
            //        Console.Write(rGraph[u, v] + " ");
            //    }
            //    Console.WriteLine();
            //}
            return (maxFlow, rGraph ).ToTuple();
            
        }
    }
    
}
