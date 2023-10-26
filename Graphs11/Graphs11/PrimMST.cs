using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    public static class Prim
    {
        static int VertexCount;
        public static List<(int,int,int)> PrimMST(this IGraph graph)
        {
            var primMST = new List<(int, int, int)>();
            var matrix = graph.AdjacencyMatrix();
            VertexCount = graph.VertexCount();
            bool[]inMST = new bool[VertexCount];
            inMST[0] = true;
            int edgeCount = 0;
            while (edgeCount<VertexCount-1)
            {
                int min = int.MaxValue, a=-1,b=-1;
                for (int i = 0; i < VertexCount; i++)
                {
                    for (int j = 0; j < VertexCount; j++)
                    {
                        if (matrix[i,j] < min && matrix[i,j]!=0 )
                        {
                            if (IsValidEdge(i,j,inMST))
                            {
                                min = matrix[i, j];
                                a = i;
                                b = j;
                                
                            }
                        }
                    }
                }
                if (a != -1 && b !=- 1)
                {
                    //mincost = mincost + min;
                    inMST[b] = inMST[a]=true;
                    primMST.Add((a+1, b+1, min));
                    edgeCount++;
                }
            }


            return primMST;
        }

        private static bool IsValidEdge(int u, int v, bool[] inMST)
        {
            if (u==v)
                return false;
            if (inMST[u]==false && inMST[v]==false)
                return false;
            else if (inMST[u]==true && inMST[v]==true)
                return false;
            return true;
        }
    }
}
