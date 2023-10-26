using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs4
{
    public static class Extensions
    {
        public static int[,] Reverse(this IGraph graph)
        {
            var graphMatrix = graph.AdjacencyMatrix();
            for (int i = 0; i < graph.VertexCount(); i++)
            {
                for (int j = 0; j < graph.VertexCount(); j++)
                {
                    var current = graphMatrix[i, j];
                    graphMatrix[i, j] = graphMatrix[j, i];
                    graphMatrix[j, i] = current;
                }
            }
            return graphMatrix;
        }
        public static void ArrayOutput(this List<int> array, StreamWriter sw)
        {
            foreach (var item in array)
            {
                sw.Write(item + "  ");
            }

        }//Array_output(int []array)
        public static void Matrix_output(this int[,] matrix, StreamWriter sw)
        {
            int p = 1;

            foreach (var item in matrix)
            {
                if (p++ % matrix.GetLength(1) == 0)
                    if (item == int.MaxValue)
                    {
                        sw.WriteLine("∞");
                    }
                    else sw.WriteLine(item);
                else
                    if (item == int.MaxValue)
                {
                    sw.Write("∞" + " ");
                }
                else sw.Write(item + " ");
            }
        }///Matrix_output
    }
}