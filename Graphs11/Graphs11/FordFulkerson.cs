using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    public static class FordFulkersonAlg
    {
        public static bool bfs(int[,] matrix, int s, int t, List<int?> parent, int vertexCount)
        {

            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);
            var visited = new List<bool>(matrix.GetUpperBound(0) + 1 );

            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                visited.Add(false);
            }

            visited[s] = true;

            while (queue.Count > 0)
            {
                int u = queue.Dequeue();
                for (int v = 0; v < matrix.GetUpperBound(0)+1; v++)
                {
                    int capacity = matrix[u,v];
                    if (!visited[v] && capacity > 0)
                    {
                        queue.Enqueue(v);
                        visited[v] = true;
                        parent[v] = u;
                    }
                }
            }

            return visited[t];
        }
        public static Tuple<int, List<List<int>>> FordFulkerson(this IGraph graph, int sink, int source)
        {
            const int INF = 10000;
            var matrix = graph.AdjacencyMatrix();
            var flow = new List<List<int>>(graph.VertexCount());

            for (int i = 0; i < graph.VertexCount(); i++)
            {
                flow.Add(new List<int>(graph.VertexCount()));
                for (int j = 0; j < graph.VertexCount(); j++)
                {
                    flow[i].Add(0);
                }
            }

            var residualGraph = graph.AdjacencyMatrix();

            var parent = new List<int?>(graph.VertexCount());
            for (int i = 0; i < graph.VertexCount(); i++)
            {
                parent.Add(null);
            }

            int maxFlow = 0;

            while (bfs(residualGraph, source, sink, parent, graph.VertexCount()))
            {
                int pathFlow = INF;
                int s = sink;
                while (s != source)
                {
                    pathFlow = Math.Min(pathFlow, residualGraph[parent[s].Value,s]);
                    s = parent[s].Value;
                }

                int v = sink;
                while (v != source)
                {
                    int u = parent[v].Value;
                    flow[u][v] += pathFlow;
                    flow[v][u] -= pathFlow;

                    for (int i = 0; i < flow.Count; i++)
                    {
                        for (int j = 0; j < flow.Count; j++)
                        {
                            residualGraph[i,j] = matrix[i,j] - flow[i][j];
                        }
                    }

                    v = parent[v].Value;
                }
                maxFlow += pathFlow;
            }

            return (maxFlow, flow).ToTuple();

        }
    }

}
