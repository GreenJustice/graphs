using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Graphs8
{
    public class HeightMap
    {
        private List<List<MapCell>> map = new List<List<MapCell>>();
        public HeightMap(string path)
        {
            string[] lines = File.ReadAllLines(path);
            int[,] rawMap;
            rawMap = new int[lines.Length, lines.Length];
            for (int i = 0; i < lines.Length; i++)
            {
                string[] temp = lines[i].Split(' ');
                for (int j = 0; j < temp.Length; j++)
                    if (temp[j] != "")
                        rawMap[i, j] = Int32.Parse(temp[j]);
            }
            for (int i = 0; i < rawMap.GetUpperBound(0) + 1; i++)
            {
                List<MapCell> vec = new List<MapCell>();
                for (int j = 0; j < rawMap.GetUpperBound(0)+1; j++)
                {
                    MapCell cell = new MapCell();
                    cell.height = rawMap[i,j];
                    cell.x = i;
                    cell.y = j;
                    vec.Add(cell);
                }
                map.Add(vec);
            }
        }
        public MapCell Get( int x, int y)
        {
            return map[x][y];
        }
        public List<MapCell> neighbors(MapCell cell)
        {
            List<MapCell> res = new List<MapCell>();
            if (cell.x>0)
            {
                res.Add(map[cell.x - 1][cell.y]);
            }
            if (cell.x < map.Count()-1)
            {
                res.Add(map[cell.x + 1][cell.y]);
            }
            if (cell.y > 0)
            {
                res.Add(map[cell.x][cell.y - 1]);
            }
            if (cell.y < map.Count()-1)
            {
                res.Add(map[cell.x][cell.y + 1]);
            }
            return res;
        }
        public int size()
        {
            return map.Count;
        }


    }
}
