using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Graphs5
{
    public static class DijkstrasAlgorithm
    {

        private static readonly int NO_PARENT = -1;
        // Функция, реализующая
        // кратчайший путь Дейкстры из одного источника
        // алгоритм для представленного графа
        // использование матрицы смежности
        public static void Dijkstra( this IGraph graph, int startVertex, int endVertex, StreamWriter sw)
        {
            var adjacencyMatrix = graph.AdjacencyMatrix();
            int nVertices = graph.VertexCount();
            // shortestDistances[i] будет содержать
            // кратчайшее расстояние от src до i
            int[] shortestDistances = new int[nVertices];

            // added[i] будет иметь значение true, если вершина i
            // включена в дерево кратчайших путей
            // или определено кратчайшее расстояние от src до
            // i
            bool[] added = new bool[nVertices];

            // Инициализируйте все расстояния следующим образом
            // БЕСКОНЕЧНО и added[] как ложное
            
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

                int nearestVertex = 0;
                if (graph.IsDirected()==true)
                {
                    nearestVertex = 0;
                }
                else
                {
                    nearestVertex = -1;
                }
                int shortestDistance = 999999;
                for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
                {
                    if ((!added[vertexIndex]) && shortestDistances[vertexIndex] < shortestDistance)
                    {
                        nearestVertex = vertexIndex;
                        shortestDistance = shortestDistances[vertexIndex];
                    }
                }

                if (nearestVertex == -1 )
                {
                    sw.WriteLine($"There is no path between the vertices {startVertex} and {endVertex}");
                    return;
                }

                added[nearestVertex] = true;

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
           if (shortestDistances[endVertex]>= 999999)
            {
                sw.WriteLine($"There is no path between the vertices {startVertex } and {endVertex }");
                return;
            }
            PrintSolution(startVertex, endVertex, shortestDistances, parents, adjacencyMatrix, sw);
        }

        // Служебная функция для печати
        // построенные расстояния
        // массив и кратчайшие пути
        private static void PrintSolution(int startVertex, int endVertex,  int[] distances, int[] parents, int[,] matrix, StreamWriter sw)
        {
            List<int> vector = new List<int>();
            int nVertices = distances.Length;
            sw.Write("Vertex\t Distance\tPath");
            
           
            for (int vertexIndex = 0; vertexIndex < nVertices; vertexIndex++)
            {
                if (vertexIndex != startVertex)
                {
                    if (vertexIndex == endVertex)
                    {
                        
                        sw.Write("\n" + startVertex + " -> ");
                        sw.Write(vertexIndex + " \t\t ");
                        sw.Write(distances[vertexIndex] + "\t\t");
                        PrintPath(vertexIndex, parents,vector);
                    }
                    
                }
            }
            for(int i = 0;i<vector.Count;i++)
            {
                if (i+1<vector.Count) sw.Write("("+vector[i] + ", " + vector[i+1] + ", " + matrix[vector[i],vector[i+1]] + ") " );
            }
        }

        // Функция для печати кратчайшего пути
        // от источника к текущему тексту
        // используя родительский массив

        private static void PrintPath(int currentVertex, int[] parents, List<int> vector)
        {

            // Base case : Source node has
            // been processed
            if (currentVertex == NO_PARENT)
            {
                return;
            }
            
            
            PrintPath(parents[currentVertex], parents,vector);
            
            vector.Add(currentVertex);
            //Console.Write(currentVertex + " ");
            
        }
    }
}
