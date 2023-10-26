using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    public static class Bipartite
    {
        private static int[] bfs(int[,] matrix, int s, int vertexCount)
        {
            
            int[] colour = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                colour[i] = -1;
            }
            List<int> queue = new List<int>();
            
            queue.Add(s);
            colour[s] = 0;
            
            while (queue.Count > 0)
            {
                int u = queue[0];
                queue.RemoveAt(0);
                for (int vertex = 0; vertex < vertexCount; vertex++)
                {
                    if (colour[vertex] == -1 && matrix[u, vertex] > 0)
                    {
                        queue.Add(vertex);
                        colour[vertex] = 1 - colour[u];
                    }
                }
            }
            return colour;
        }
        public static int Matching(this IGraph graph)
        {
            
            var verCount = graph.VertexCount();
            var graphMatrix = graph.AdjacencyMatrix();
            var colours = bfs(graphMatrix, 0, verCount);
            int[,] flowMatrix = new int[verCount + 2, verCount + 2];
            int source = verCount, sink = verCount+1;
            for (int i = 0;i < verCount;i++)
            {
                for(int j = 0; j < verCount; j++)
                {
                    flowMatrix[i, j] = graphMatrix[i, j];
                }
            }
            for (int i = 0; i < verCount; i++)
            {
                for (int j = 0; j < verCount; j++)
                {
                    if (flowMatrix[i,j] == 0)
                    {
                        continue;
                    }

                    if (colours[i] == 0)
                    {
                        flowMatrix[j,i] = 0;
                    }
                }
            }
            for (int i = 0; i<colours.Length; i++)
            {
                if (colours[i] == 0)
                {
                    flowMatrix[sink, i] = 1;
                }
            }
            for (int i = 0; i< colours.Length; i++)
            {
                if (colours[i] == 1)
                {
                    flowMatrix[i, source] = 1;
                }
            }
            //for (int i = 0; i < colours.Length; i++)
            //{
            //    if (colours[i] == 0)
            //    {
            //        flowMatrix[i, source] = 1; 
            //    }
            //    if (colours[i] == 1)
            //    {
            //        flowMatrix[sink, i] = 1;
            //    }
            //}
            //for (int i = 0; i < flowMatrix.GetUpperBound(0) + 1; i++)
            //{
            //    for (int j = 0; j < flowMatrix.GetUpperBound(0) + 1; j++)
            //    {
            //        Console.Write(flowMatrix[i, j] + " ");
            //    }
            //    Console.WriteLine();
            //}
               
            var newGraph = new GraphMatrix(flowMatrix);
            var result = newGraph.FordFulkerson(source, sink);
            var matrix = result.Item2; 
            HashSet<int> visited = new HashSet<int>();
            List<(int, int)> matching = new List<(int, int)>();
            //List<int> secondFraction = new List<int> { };
            //List<int> firstFraction = new List<int>{ };
            for (int i = 0; i < verCount; i++)
            {
                for (int j = 0; j < verCount; j++)
                {
                    if(graph.IsEdge(i,j))
                    {
                        if (matrix[i][j]==1 && !visited.Contains(i) && !visited.Contains(j))
                        {
                            visited.Add(i);
                            visited.Add(j);
                            matching.Add((i, j));
                            Console.WriteLine(i + " " + j);
                        }
                    }
                }
                
            }
            for (int i = 0; i < matrix.Count; i++)
            {
                for (int j = 0; j < matrix.Count; j++)
                {
                    Console.Write(matrix[i][j] + " ");
                }
                Console.WriteLine();
            }



            return result.Item1;
        }
    }
}
