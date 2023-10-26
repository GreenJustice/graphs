
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs10
{

    using Vertex = System.Int32;
    internal class GraphMatrix : IGraph
    {
        public int[,] num;

        public GraphMatrix(string path, string key)
        {
            string[] lines = File.ReadAllLines(path);
            if (key=="-m")
            {
                num = new int[lines.Length, lines.Length];
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] temp = lines[i].Split(' ');
                    for (int j = 0; j < temp.Length; j++)
                        if (temp[j] != "")
                            num[i, j] = Int32.Parse(temp[j]);
                }
            }
            if (key == "-e")
            {
                int max = 0;
                List<(int, int, int)> graph = new List<(int, int, int)> { };
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
                num = new int[max, max];
                for (int i = 0; i < graph.Count; i++)
                {
                    num[graph[i].Item1, graph[i].Item2] = graph[i].Item3;
                }
            }
            if (key == "-l")
            {
                int k = 0;
                var graph = new List<List<int>>();
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
                int[,] num = new int[graph.Count, graph.Count];
                for (int i = 0; i < graph.Count; i++)
                    for (int j = 0; j < graph[i].Count; j++)
                    {
                        num[i, graph[i][j]] = 1;
                    }
            }
            

        }//Конструктор Graph
        public GraphMatrix(int [,] matrix)
        {
            num = matrix;
        }
        public void GetGraph()
        {
            int p = 1;

            foreach (var item in num)
            {
                if (p++ % num.GetLength(1) == 0)
                    Console.WriteLine(item);
                else
                    Console.Write(item + "\t");
            }
        }//GetGraph
        public int Weight(Vertex vi, Vertex vj)
        {
            if (num[vi, vj] == 0)
            {
                Console.WriteLine("There is no such edge");
            }
            return num[vi, vj];
        }//weight
        public bool IsEdge(Vertex vi, Vertex vj)
        {
            if (num[vi, vj] != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }//is_edge
        public int[,] AdjacencyMatrix()
        {
            
            int[,] copy = num.Clone() as int[,];
            return copy;
        }//adjacency_matrix

        public List<int> AdjacencyList(Vertex v)
        {
            List<int> adjacency = new List<int> { };
            for (Vertex i = 0; i < num.GetUpperBound(0) + 1; i++)
            {
                if (i == v)
                {

                    for (Vertex j = 0; j < num.GetUpperBound(0) + 1; j++)
                    {
                        if (num[i, j] != 0)
                        {
                            adjacency.Add(j);
                        }
                    }
                    
                    return adjacency;



                }
            }
            return adjacency;
        }//adjacency_list

        public List<(int, int, int)> ListOfEdges()
        {
            List<(int, int, int)> edges = new List<(int, int, int)>();
            int k = 1;

            //Console.Write("list_of_edges is: ");
            for (int i = 0; i < num.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < num.GetUpperBound(0) + 1; j++)
                {
                    if (num[i, j] != 0)
                    {
                        edges.Add((i,j,num[i,j]));
                    }
                }
            }
            return edges;
        }//list_of_edges()
        public List<(int, int, int)> ListOfEdges(Vertex v)
        {
            List<(int, int, int)> edges = new List<(int, int, int)>();
            //Console.Write("list_of_edges Vertex " + v + " is: ");
            for (int j = 0; j < num.GetUpperBound(0) + 1; j++)
            {
                if (num[v, j] != 0)
                {
                    edges.Add((v, j, num[v, j]));
                }
            }
            return edges;
        }//list_of_edges(Vertex v)
        public bool IsDirected()
        {
            for (int i = 0; i < num.GetUpperBound(0) + 1; i++)
            {
                for (int j = i + 1; j < num.GetUpperBound(0) + 1; j++)
                {
                    if (num[i, j] != num[j, i])
                    {
                        return true;
                    }
                }

            }
            return false;
        }//is_directed()

        



        //}//VectorOfDegrees()
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int VertexCount()
        {
            return num.GetUpperBound(0) + 1;
        }
        public static void Array_output(int []array)
        {
            foreach (var item in array)
            {
                Console.Write(item + "    ");
            }
            
        }//Array_output(int []array)
        public static void Matrix_output(int [,]matrix)
        {
            int p = 1;

            foreach (var item in matrix)
            {
                if (p++ % matrix.GetLength(1) == 0)
                    Console.WriteLine(item );
                else
                    Console.Write(item + "    ");
            }
        }///Matrix_output

    }
}
