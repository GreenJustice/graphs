using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Graphs8
{
    public static class Kruskal
    {
       
        public class subset
        {
            public int parent, rank;
        };
        
        private static int find(subset[] subsets, int i)
        {
            // Найдeм корень и сделаем его как
            // родительский элемент i
            if (subsets[i].parent != i)
                subsets[i].parent = find(subsets, subsets[i].parent);

            return subsets[i].parent;
        }

        // Функция, которая выполняет объединение
        // двух наборов x и y 
        private static void Union(subset[] subsets, int x, int y)
        {
            int xroot = find(subsets, x);
            int yroot = find(subsets, y);

            // Присоединим дерево меньшего ранга к корню
            // дерева высокого ранга 
            if (subsets[xroot].rank < subsets[yroot].rank)
                subsets[xroot].parent = yroot;
            else if (subsets[xroot].rank > subsets[yroot].rank)
                subsets[yroot].parent = xroot;

            // Если ранги одинаковы, то сделаем один из них корневым
            // и увеличем его ранг на единицу
            else
            {
                subsets[yroot].parent = xroot;
                subsets[xroot].rank++;
            }
        }
        public static List<(int, int, int)> KruskalMst(this IGraph graph)
        {
            var edge = graph.ListOfEdges();
            var vertexCount = graph.VertexCount();
            List<(int, int, int)> result = new List<(int, int, int)>();
            int e = 0, i = 0;
            var query = edge.OrderBy(t => t.Item3).ToList(); //отсортируем список ребер
            
            subset[] subsets = new subset[vertexCount];
            for ( i = 0; i < vertexCount; i++)
            {
                subsets[i] = new subset();
            }
            for (int v = 0; v < vertexCount; ++v)
            {
                subsets[v].parent = v;
                subsets[v].rank = 0;
            }
            i = 0;
            while (e<vertexCount-1)
            {
                var nextEdge = (-1,-1,-1);
                nextEdge = query[i];

                i++;
                //e++;
                int x = find(subsets, nextEdge.Item1);
                int y = find(subsets, nextEdge.Item2);
                if (x!=y)
                {
                    result.Add(nextEdge);
                    e++;
                    Union(subsets, x, y);
                }
            }
            return result;
        }
        
    }
}
