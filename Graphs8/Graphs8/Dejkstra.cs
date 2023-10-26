using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Linq;

namespace Graphs8
{
    public static class DijkstrasAlgorithm
    {

        private static readonly int NO_PARENT = -1;

        public static void Dijkstra(this IGraph graph, int startVertex, StreamWriter sw)
        {
            int[,] adjacencyMatrix = graph.AdjacencyMatrix();
            
            int nVertices = graph.VertexCount();
            // shortestDistances[i] будет содержать
            // кратчайшее расстояние от src до i
            int[] shortestDistances = new int[nVertices];

            // added[i] будет иметь значение true, если вершина i
            // включена в дерево кратчайших путей
            // или определено кратчайшее расстояние от src до
            // i
            bool[] added = new bool[nVertices];

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                shortestDistances[vertexIndex] = int.MaxValue;
                added[vertexIndex] = false;
            }


            // Родительский массив для хранения кратчайшего
            // дерево путей
            int[] parents = new int[nVertices];

            // У начальной вершины нет родительской
            parents[startVertex] = NO_PARENT;
            shortestDistances[startVertex] = 0;
            // Найдем кратчайший путь для всех
            // вершин
            for (int i = 1; i < nVertices; i++)
            {

                // Выберем вершину с минимальным расстоянием
                // из набора вершин, которые еще не
                // обработаны. nearestVertex
                // всегда равен startNode в
                // первой итерации.
                int nearestVertex = -1;
                int shortestDistance = int.MaxValue;
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    if ((!added[vertexIndex]) && shortestDistances[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                // Отметим выбранную вершину как
                // обработанную
                if (nearestVertex == -1)
                {

                    return;
                }

                added[nearestVertex] = true;

                // Обновим значение dist для 
                // соседних вершин выбранной вершины.
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    int edgeDistance = adjacencyMatrix[nearestVertex, vertexIndex];

                    if (edgeDistance > 0 && ((shortestDistance + edgeDistance) < shortestDistances[vertexIndex]))
                    {
                        parents[vertexIndex] = nearestVertex;
                        shortestDistances[vertexIndex] = shortestDistance + edgeDistance;
                    }
                }
            }

            PrintSolution(startVertex, shortestDistances, parents, adjacencyMatrix, sw);
        }

        // Служебная функция для печати
        // построенные расстояния
        // массив и кратчайшие пути
        private static void PrintSolution(int startVertex, int[] distances, int[] parents, int[,] matrix, StreamWriter sw)
        {
            int nVertices = distances.Length;
            sw.Write("Vertex\t Distance\tPath");

            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != startVertex)
                {


                    sw.Write("\n" + startVertex + " -> ");
                    sw.Write(vertexIndex + " \t\t ");
                    sw.Write(distances[vertexIndex] + "\t\t");
                    PrintPath(vertexIndex, parents);


                }

            }
        }

        // Функция для печати кратчайшего пути
        // от источника к текущему тексту
        // используя родительский массив

        private static void PrintPath(int currentVertex, int[] parents)
        {

            // Base case : Source node has
            // been processed
            if (currentVertex == NO_PARENT)
            {
                return;
            }


            PrintPath(parents[currentVertex], parents);

            //Console.Write(currentVertex + " ");

        }
    }
}
