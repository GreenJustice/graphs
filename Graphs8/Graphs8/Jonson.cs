using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs8
{
    public static class Jonson
    {
        private static readonly int NO_PARENT = -1;
        private static (List<int>, bool) BellmanFord(List<(int,int,int)> edges, int s)
        {
            const int INF = 10000;
            List<int> dist = Enumerable.Repeat(INF, s + 1).ToList();
            dist[s] = 0;

            for (int i = 0; i < s; i++)
            {
                edges.Add(new (s, i, 0));
            }

            for (int i = 0; i < s; i++)
            {
                foreach (var edge in edges)
                {
                    if (dist[edge.Item1] != INF && dist[edge.Item1] + edge.Item3 < dist[edge.Item2])
                    {
                        dist[edge.Item2] = dist[edge.Item1] + edge.Item3;
                    }
                }
            }

            bool negativeCycle = edges.Any(edge => dist[edge.Item1] + edge.Item3 < dist[edge.Item2]);

            dist.RemoveAt(dist.Count - 1);

            return (dist, negativeCycle);
        }

        private static int MinDistance(List<int> dist, List<bool> marked)
        {
            const int INF = 10000;

            int minimum = INF;
            int minVertex = 0;
            for (int vertex = 0; vertex < dist.Count; vertex++)
            {
                if (minimum > dist[vertex] && !marked[vertex])
                {
                    minimum = dist[vertex];
                    minVertex = vertex;
                }
            }
            return minVertex;
        }

        private static List<int> Dijkstra(int[,] graph, List<List<int>> modifiedGraph, int src, List<int> fordDist)
        {
            const int INF = 10000;

            int numVertices = (int)(Math.Sqrt(graph.Length));
            List<bool> marked = Enumerable.Repeat(false, numVertices).ToList();
            List<int> dist = Enumerable.Repeat(INF, numVertices).ToList();

            dist[src] = 0;

            for (int i = 0; i < numVertices; i++)
            {
                int curVertex = MinDistance(dist, marked);
                marked[curVertex] = true;

                for (int vertex = 0; vertex < numVertices; vertex++)
                {
                    if (!marked[vertex] && dist[vertex] > dist[curVertex] + modifiedGraph[curVertex][vertex] && graph[curVertex,vertex] != 0)
                    {
                        dist[vertex] = dist[curVertex] + modifiedGraph[curVertex][vertex];
                    }
                }
            }

            for (int i = 0; i < dist.Count; i++)
            {
                dist[i] += fordDist[i] - fordDist[src];
            }

            return dist;
        }

        public static List<List<int>> Johnson(this IGraph graph)
        {
            var origMatrix = graph.AdjacencyMatrix();

            List<(int,int,int)> edges = graph.ListOfEdges();
            (List<int> modifyWeights, bool negativeCycle) = BellmanFord(edges, graph.VertexCount());

            if (negativeCycle)
            {
                throw new Exception("Graph contains a negative cycle.");
            }

            List<List<int>> matrix = Enumerable.Repeat(0, graph.VertexCount()).Select(x => Enumerable.Repeat(0, graph.VertexCount()).ToList()).ToList();

            for (int i = 0; i < graph.VertexCount(); i++)
            {
                for (int j = 0; j < graph.VertexCount(); j++)
                {
                    if (origMatrix[i,j] != 0)
                    {
                        matrix[i][j] = origMatrix[i,j] + modifyWeights[i] - modifyWeights[j];
                    }
                }
            }

            List<List<int>> result = new List<List<int>>();

            for (int i = 0; i < graph.VertexCount(); i++)
            {
                result.Add(Dijkstra(origMatrix, matrix, i, modifyWeights));
            }

            return result;
        }
    }
}
