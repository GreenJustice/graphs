using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Graphs8
{
    
    public static class AstarRealiz
    {
        public static Tuple<List<MapCell>, int, int> AStar(this HeightMap map, MapCell start, MapCell end, Func<MapCell, MapCell, int> heuristic)
        {
            int viewed=0;
            HashSet<Tuple<int,int>> viewVertex = new HashSet<Tuple<int,int>>();
            int count = 0;
            PriorityQueue<MapCell, int> openSet = new PriorityQueue<MapCell, int>();
            start.priority = heuristic(start, end);
            List<List<MapCell>> cameFrom = new List<List<MapCell>>();
            List<List<int>> costSoFar = new List<List<int>>();
            List<MapCell> empty = new List<MapCell>();
            for (int i = 0; i<map.size();i++)
            {
                List<MapCell> cameLine = new List<MapCell>();
                List<int> costLine = new List<int>();
                for (int j = 0; j < map.size();j++) 
                {
                    MapCell cell = new MapCell();
                    cell.x = -1;
                    cell.y = -1;
                    cell.height = -1;
                    cell.priority = -1;
                    cameLine.Add(cell);
                    costLine.Add(-1);
                }
                cameFrom.Add(cameLine);
                costSoFar.Add(costLine);
            }

            openSet.Enqueue(start,heuristic(start,end));
            costSoFar[start.x][start.y] = 0;

            while (openSet.Count!=0)
            {
                MapCell current = openSet.Dequeue();

                if (current.PositionEqual(end))
                {
                    List<MapCell> path = new List<MapCell>();
                    path.Add(current);
                    MapCell prev = cameFrom[current.x][current.y]; 
                    while (prev.x != -1) 
                    {
                        path.Add(prev);
                        prev = cameFrom[prev.x][prev.y]; 
                    }
                    path.Reverse();
                    return (path, count, viewed).ToTuple();
                }
                foreach (var neighbor in map.neighbors(current))
                {
                    if (!viewVertex.Contains((neighbor.x, neighbor.y).ToTuple()))
                    {
                        count++;
                    }
                    viewVertex.Add((neighbor.x, neighbor.y).ToTuple());
                    int heightDif = Math.Abs(current.height - neighbor.height);
                    int newCost = costSoFar[current.x][current.y] + 1 + heightDif;
                    if (costSoFar[neighbor.x][neighbor.y] == -1 || costSoFar[neighbor.x][neighbor.y]>newCost)
                    {
                        costSoFar[neighbor.x][neighbor.y] = newCost;
                        MapCell newCell = new MapCell();
                        newCell.x = neighbor.x;
                        newCell.y = neighbor.y;
                        newCell.priority = newCost + heuristic(neighbor, end);
                        newCell.height = neighbor.height;
                        openSet.Enqueue(newCell,newCell.priority);
                        cameFrom[neighbor.x][neighbor.y] = current;
                    }
                }
            }
            return (empty, -1, 0).ToTuple();
        }
        public static int PathCost(this HeightMap map, List<MapCell> path)
        {
            int cost = 0;
            for (int i = 0;i < path.Count-1; i++)
            {
                MapCell current = path[i+1];
                MapCell next = path[i];
                int dif = Math.Abs(map.Get(current.x, current.y).height - map.Get(next.x, next.y).height);
                cost += 1 + dif;
            }
            return cost;
        }
    }
}
