using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs2
{
    public static class DegreeVector
    {
        public static (List<int>, List<int>) VectorOfDegreesOriented(this IGraph graph)
        {
            if (graph.IsDirected() == false)
                throw new Exception("Graph must be directed!");
            var edges = graph.ListOfEdges();
            int[] vector1 = new int[graph.VertexCount()];
            int[] vector2 = new int[graph.VertexCount()];

                for (int i = 0; i < graph.VertexCount(); i++)
                {
                    for (int j = 0; j < edges.Count; j++)
                    {
                        if (edges[j].Item1 == i)
                            vector2[i]++;
                        if (edges[j].Item2 == i)
                            vector1[i]++;
                    }
                }


            return (vector1.ToList(), vector2.ToList());


        }//VectorOfDegrees()

        public static List<int> VectorOfDegreesNotOriented(this IGraph graph)
        {
            if (graph.IsDirected() == true)
                throw new Exception("Graph must be not directed!");
            return Enumerable.Range(0,graph.VertexCount()).Select(x => graph.AdjacencyList(x).Count).ToList();
        }
        public static List<int> Excentr(int[,] distance)
        {
            List<int> extr = Enumerable.Repeat(0, distance.GetUpperBound(0) + 1).ToList();
            for (int i = 0; i < distance.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < distance.GetUpperBound(0) + 1; j++)
                {
                    if (distance[i, j] > extr[i] && distance[i, j] != int.MaxValue)
                    {
                        extr[i] = distance[i,j];
                    }
                    if (distance[i, j] == int.MaxValue)
                        return new List<int>();

                }
            }
            return extr;
        }//Excentr(int [,]distance)
        public static int GetRadius(List<int> extr)
        {
            int Radius = 0;
            Radius = extr.Min();
            return Radius;
        }//GetRadius(int []extr)
        public static int GetDiameter(List<int> extr)
        {
            int Diameter = 0;
            Diameter = extr.Max();
            return Diameter;
        }//GetDiameter(int[] extr)
        public static List<int> CentralVertices(List<int> extr)
        {
            List<int> vertices = new List<int>();
            for (int i = 0; i < extr.Count; i++)
            {
                if (extr[i] == extr.Min())
                    vertices.Add(i);
            }
            return vertices;
        }
        public static List<int> PeripheralVertices(List<int> extr)
        {
            List<int> vertices = new List<int>();
            for (int i = 0; i < extr.Count; i++)
            {
                if (extr[i] == extr.Max())
                    vertices.Add(i);
            }
            return vertices;
        }

    }
}
