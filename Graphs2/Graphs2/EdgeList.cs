using Graphs2;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs2
{
    using Vertex = System.Int32;
    internal class EdgeList : IGraph
    {
        int max = 0;
        public List<(int, int, int)> graph = new List<(int, int, int)> { };
        public EdgeList(string path)
        {
            string[] lines = File.ReadAllLines(path);
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                int vi = Int32.Parse(temp[0]);
                int vj = Int32.Parse(temp[1]);
                if (vi > max)
                {
                    max = vi;
                }
                if (vj > max)
                {
                    max = vj;
                }
                int w;
                if (temp.Length == 3)
                {
                    if (temp[2] == "")
                    {
                        w = 1;
                    }

                    else
                    {
                        w = Int32.Parse(temp[2]);
                    }

                }
                else if (temp.Length > 3)
                {
                    if (temp[2] != "") w = Int32.Parse(temp[2]);
                    else w = 1;

                }
                else
                {
                    w = 1;
                }

                graph.Add((vi - 1, vj - 1, w));
            }

        }//Конструктор
        public int Weight(Vertex vi, Vertex vj)
        {
            for (int i = 0; i < graph.Count; i++)
            {
                if ((graph[i].Item1 == vi) && (graph[i].Item2 == vj))
                {
                    return graph[i].Item3;
                }
            }
            return 0;
        }//weight(Vertex vi, Vertex vj)
        public bool IsEdge(Vertex vi, Vertex vj)
        {
            if (Weight(vi, vj) != 0)
            {
                return true;
            }
            else return false;
        }

        public int[,] AdjacencyMatrix()
        {


           
            int[,] matrix = new int[max, max];
            for (int i = 0; i < graph.Count; i++)
            {
                matrix[graph[i].Item1, graph[i].Item2] = graph[i].Item3;
            }
            //Matrix_output(matrix);
            return matrix;


        }//adjacency_matrix()
        public List<int> AdjacencyList(Vertex v)
        {
            return graph.Where(x => x.Item1 == v).Select(x => x.Item2).ToList();             //Select
        }//adjacency_list(Vertex v)
        public List<(int, int, int)> ListOfEdges()
        {
            return graph;
        }//list_of_edges()
        public List<(int, int, int)> ListOfEdges(Vertex v)
        {
            return graph.Where(e => e.Item1 == v).ToList();                                 //Where
        }//list_of_edges(Vertex v)
        public bool IsDirected()
        {
            int[,] matrix = AdjacencyMatrix();
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
                for (int j = 0; j < matrix.GetUpperBound(0) + 1; j++)
                    if (matrix[i, j] != matrix[j, i])
                        return true;
            return false;
        }//is_directed()

        public void PrintGraph()
        {
            for (int i = 0; i < graph.Count; i++)
            {
                Console.WriteLine($"{graph[i].Item1} {graph[i].Item2} {graph[i].Item3}");
            }
        }//PrintGraph()

        public int VertexCount()
        {
            return max;
        }

        //}//VectorOfDegrees()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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

    }
}
