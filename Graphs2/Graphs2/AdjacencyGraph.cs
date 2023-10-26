using Graphs2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs2
{
    using Vertex = System.Int32;
    internal class AdjacencyGraph : IGraph
    {
        public List<List<int>> graph;
        //private int max;
        public AdjacencyGraph(string path)
        {
            int k = 0;
            graph = new List<List<int>>();
            string[] lines = File.ReadAllLines(path);
            foreach (string line in lines)
            {
                string[] temp = line.Split(' ');
                graph.Add(new List<int>());
                for (int j = 0; j < temp.Length; j++)
                {
                    if (temp[j] != "")
                    {
                        graph[k].Add(Int32.Parse(temp[j]) - 1);
                        
                    }
                }
                k++;
            }
        }
        public int Weight(Vertex vi, Vertex vj)
        {
            vi = vi - 1;
            for (int i = 0; i < graph.Count; i++)
            {
                if (i == vi)
                {
                    for (int j = 0; j < graph[i].Count; j++)
                    {
                        if ((graph[i][j] == vj))
                            return 1;
                    }
                }


            }
            return 0;
        }
        public bool IsEdge(Vertex vi, Vertex vj)
        {
            if (Weight(vi, vj) != 0)
                return true;
            else
                return false;
        }
        public int[,] AdjacencyMatrix()
        {
            int[,] matrix = new int[graph.Count, graph.Count];
            for (int i = 0; i < graph.Count; i++)
                for (int j = 0; j < graph[i].Count; j++)
                {
                    matrix[i, graph[i][j]] = 1;
                }
            return matrix;
        }
        public List<int> AdjacencyList(Vertex v)
        {
            return graph[v];
        }
        public List<(int, int, int)> ListOfEdges()
        {
            List<(int, int, int)> edges = new List<(int, int, int)>();
            for (int i = 0; i < graph.Count; i++)
            {
                for (int j = 0; j < graph[i].Count; j++)
                {
                    edges.Add((i, graph[i][j], 1));
                }
            }

            return edges;
        }
        public List<(int, int, int)> ListOfEdges(Vertex v)
        {
            return graph[v].Select(x => (v, x, 1)).ToList();                      //Select
        }
        public bool IsDirected()
        {
            int[,] matrix = AdjacencyMatrix();
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                    if (matrix[i, j] != matrix[j, i])
                        return true;
            return false;
        }

        


        public static void Array_output(int[] array)
        {
            foreach (var item in array)
            {
                Console.Write(item + "    ");
            }

        }//Array_output(int []array)

        public static void Matrix_output(int[,] matrix)
        {
            int p = 1;

            foreach (var item in matrix)
            {
                if (p++ % matrix.GetLength(1) == 0)
                    Console.WriteLine(item);
                else
                    Console.Write(item + "    ");
            }
        }///Matrix_output

        public void GetGraph()
        {
            for (int i = 0; i < graph.Count; i++)
            {

                for (int j = 0; j < graph[i].Count; j++)
                    Console.Write(graph[i][j] + " ");
                Console.WriteLine();
            }
        }

        public int VertexCount()
        {
            return graph.Count;
        }
    }
}
