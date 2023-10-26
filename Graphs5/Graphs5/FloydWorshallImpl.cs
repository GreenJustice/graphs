using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs5
{
    public static class FloydWorshallImpl 
    {
        public static int[,] FloydWarshall(this IGraph graph)
        {
            var num = graph.AdjacencyMatrix();
            int[] Extr = new int[num.GetUpperBound(0) + 1];
            //int Radius, Diameter;
            int[,] distance = new int[num.GetUpperBound(0) + 1, num.GetUpperBound(0) + 1];
            distance = num;
            for (int i = 0; i < num.GetUpperBound(0) + 1; ++i)
            {
                for (int j = 0; j < num.GetUpperBound(0) + 1; ++j)
                {
                    if (i != j && distance[i, j] == 0)
                    {
                        distance[i, j] = int.MaxValue;
                    }
                }
            }
            for (int k = 0; k < num.GetUpperBound(0) + 1; ++k)
            {
                for (int i = 0; i < num.GetUpperBound(0) + 1; ++i)
                {
                    for (int j = 0; j < num.GetUpperBound(0) + 1; ++j)
                    {
                        if (distance[i, k] != 0 && distance[k, j] != 0 && distance[i, k] != int.MaxValue && distance[k, j] != int.MaxValue)
                        {
                            if (distance[i, k] + distance[k, j] < distance[i, j])
                                distance[i, j] = distance[i, k] + distance[k, j];

                        }

                    }
                }
            }

           



            return distance;
        }//FloydWarshall()
       
    }
}
