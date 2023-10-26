using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Graphs6
{
    public static class Boruvka
    {
        //функция для поиска корня элемента i
        private static int Find(List<int> parent, int i)
        {
            if (parent[i] == i)
            {
                return i;
            }
            return Find(parent, parent[i]);
        }

        // Функция, которая выполняет объединение двух остовных деревьев x и y
        private static void UnionSet(List<int> parent, List<int> rank,
                              int x, int y)
        {
            int xroot = Find(parent, x);
            int yroot = Find(parent, y);

            // Прикрепляем дерево меньшего ранга к корню дерева высшего ранга
            if (rank[xroot] < rank[yroot])
            {
                parent[xroot] = yroot;
            }
            else if (rank[xroot] > rank[yroot])
            {
                parent[yroot] = xroot;
            }

            // Если ранги одинаковы, то делаем один корневой и
            // увеличим его ранг на единицу
            else
            {
                parent[yroot] = xroot;
                rank[xroot]++;
            }
        }


        // TОсновная функция для построения MST 
       
        public static List<(int, int, int)> BoruvkaMST(this IGraph graph)
        {
            var edges = graph.ListOfEdges();
            List<int> parent = new List<int>();

            // Массив для хранения индекса самого дешевого ребра подмножества
            //. В нем хранятся [u,v,w] для каждого компонента
            List<int> rank = new List<int>();
            List<List<int>> cheapest = new List<List<int>>();
            var result = new List<(int, int, int)>();
            // Изначально существует vertexCount разных деревьев.
            // Наконец, останется одно дерево, которое будет MST
            int numTrees = graph.VertexCount();


            // Создаeм vertexCount подмножеств с отдельными элементами
            for (int node = 0; node < graph.VertexCount(); node++)
            {
                parent.Add(node);
                rank.Add(0);
                cheapest.Add(new List<int> { -1, -1, -1 });
            }

            // Продолжаeм комбинировать компоненты до тех пор, пока все
            // компоненты не будут объединены в один MST
            while (numTrees > 1)
            {

                // Пройдем по всем ребрам и обновим 
                // параметры каждого компонента
                for (int i = 0; i < edges.Count;i++)
                    
                {

                    // Найдем компоненты двух углов
                    // текущего края
                    int u = edges[i].Item1, v = edges[i].Item2,
                        w = edges[i].Item3;
                    int set1 = Find(parent, u),
                        set2 = Find(parent, v);

                    // Если два угла текущего ребра принадлежат
                    // одному и тому же набору, игнорируем текущее ребро. Еще раз проверим
                    // если текущий край ближе к предыдущему
                    // самые дешевые ребра set1 и set2
                    if (set1 != set2)
                    {
                        if (cheapest[set1][2] == -1
                            || cheapest[set1][2] > w)
                        {
                            cheapest[set1]
                                = new List<int> { u, v, w };
                        }
                        if (cheapest[set2][2] == -1
                            || cheapest[set2][2] > w)
                        {
                            cheapest[set2]
                                = new List<int> { u, v, w };
                        }
                    }
                }

                // Рассмотрим выбранные выше самые дешевые ребра и
                // добавим их в MST
                for (int node = 0; node < graph.VertexCount(); node++)
                {

                    // Проверим, существует ли самый дешевый вариант для текущего набора
                    if (cheapest[node][2] != -1)
                    {
                        int u = cheapest[node][0],
                            v = cheapest[node][1],
                            w = cheapest[node][2];
                        int set1 = Find(parent, u),
                            set2 = Find(parent, v);
                        if (set1 != set2)
                        {
                          
                            UnionSet(parent, rank, set1, set2);
                            result.Add((u + 1, v + 1, w));
                            numTrees--;
                        }
                    }
                }
                for (int node = 0; node < graph.VertexCount(); node++)
                {
                    // Обновим cheapest
                    cheapest[node][2] = -1;
                }
            }
            return result;
            
        }
    }
}