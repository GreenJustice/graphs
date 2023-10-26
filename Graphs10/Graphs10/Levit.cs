using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs10
{
    public static class LevitAlgorithm
    {
        public static Tuple<int[],bool> LevitFindShortestPath(this IGraph graph, int sourceVertex)
        {
            int n = graph.VertexCount();
            int[] dist = new int[n];
            for (int i = 0; i < n; i++)
            {
                dist[i] = int.MaxValue;
            }
            dist[sourceVertex] = 0;

            int[] queueKind = new int[n];
            queueKind[sourceVertex] = 1;

            Queue<int> m0 = new Queue<int>();
            Queue<int> m1 = new Queue<int>();
            Queue<int> m1Priority = new Queue<int>();
            Queue<int> m2 = new Queue<int>();

            m1.Enqueue(sourceVertex);

            for (int i = 0; i < n; i++)
            {
                if (i != sourceVertex)
                {
                    m0.Enqueue(i);
                }
            }

            while (m1.Count > 0 || m1Priority.Count > 0)
            {
                int v = m1Priority.Count > 0 ? m1Priority.Dequeue() : m1.Dequeue();

                m2.Enqueue(v);

                foreach ((int, int, int) u in graph.ListOfEdges(v))
                {
                    if (m0.Contains(u.Item2))
                    {
                        dist[u.Item2] = dist[v] + u.Item3;
                        m0 = new Queue<int>(m0.Where(k => k != u.Item2));
                        m1.Enqueue(u.Item2);
                    }
                    if (m1.Contains(u.Item2))
                    {
                        dist[u.Item2] = System.Math.Min(dist[u.Item2], dist[v] + u.Item3);
                    }
                    if (m2.Contains(u.Item2) && dist[u.Item2] > dist[v] + u.Item3)
                    {
                        dist[u.Item2] = dist[v] + u.Item3;
                        m2 = new Queue<int>(m2.Where(k => k != u.Item2));
                        m1Priority.Enqueue(u.Item1);
                    }
                }
            }
            var edges = graph.ListOfEdges();
            var negative = edges.Any(e => dist[e.Item1] + e.Item3 < dist[e.Item2]);
            return Tuple.Create(dist,negative);

        } 
    }

      
}
