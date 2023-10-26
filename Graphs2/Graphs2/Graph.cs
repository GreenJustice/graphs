
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs2
{

    using Vertex = System.Int32;
    internal class GraphMatrix : IGraph
    {
        public int[,] num;

        public GraphMatrix(string path)
        {
           

                string[] lines = File.ReadAllLines(path);
                
                num = new int[lines.Length, lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                    if (temp[j] != "")
                        num[i, j] = Int32.Parse(temp[j]);

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
