using Graphs8;
using System.Collections.Specialized;

internal class Program
{
    static void ShowHelp()
    {
        Console.WriteLine("Performed by: Vasilii Siniakov\n Group: М3О-225Бк-21 \n Task №7 \n Keys: \n -m - The map is read from the adjacency matrix \n -n - start Cell  \n -d - end Cell");
    }
    static int Pow2(int x)
    {
        return x * x;
    }
    static int HeuristicManhattan(MapCell a,  MapCell b)
    {
        return Math.Abs(b.x-a.x) + Math.Abs(b.y-a.y);
    }
    static int HeuristicChebyshev(MapCell a, MapCell b)
    {
        return Math.Max(Math.Abs(b.x-a.x), Math.Abs(b.y-a.y));
    }
    static int HeuristicEuclidean(MapCell a, MapCell b)
    {
        return (int)Math.Sqrt(Pow2(b.x - a.x) + Pow2(b.y - a.y));
    }
    static int HeuristicDijkstra(MapCell a, MapCell b)
    {
        return 0;
    }
    static void Main(string[] args)
        {
            int flag = 0;
            HeightMap? map = null;
            StreamWriter? sw = null;
            int startX = 0, startY = 0;
            int endX = 0, endY = 0;
           

            for (int i = 0; i < args.Length; i++)
            {
                for (int j = 0; j < args.Length; j++)
                    if (args[j] == "-h")
                    {
                        ShowHelp();
                        flag = 1;
                        break;
                    }
                if (flag == 1)
                {
                    break;
                }
            }
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-m")
                {
                    map = new HeightMap(args[i + 1]);
                }
                else if (args[i] == "-o")
                {
                    sw = new StreamWriter(args[i + 1]);
                    sw.AutoFlush = true;
                }
                else if (args[i]=="-n")
            {
                startX = Convert.ToInt32(args[i + 1]);
                startY = Convert.ToInt32(args[i + 2]);
            }
                else if (args[i] == "-d")
            {
                endX = Convert.ToInt32(args[i + 1]);
                endY = Convert.ToInt32(args[i + 2]);
            }
               

            }
            if (map == null)
            {
                return;
            }
            if (sw == null)
            {
                sw = new StreamWriter(Console.OpenStandardOutput());
                sw.AutoFlush = true;
                Console.SetOut(sw);
            }
            flag = 0;
        List<Tuple<string, Func<MapCell, MapCell, int>>> heuristics = new List<Tuple<string, Func<MapCell, MapCell, int>>>()
        {
            Tuple.Create("Heuristic Manhattan", HeuristicManhattan),
            Tuple.Create("Heuristic Chebyshev", HeuristicChebyshev),
            Tuple.Create("Heuristic Euclidean", HeuristicEuclidean),
            Tuple.Create("Heuristic Dijkstra", HeuristicDijkstra),
        };
        foreach (var item in heuristics)
        {
            
            var res = map.AStar( map.Get(startX, startY), map.Get(endX, endY), item.Item2);
            var shortestPath = res.Item1;
            var count = res.Item2;
            var size = map.size() * map.size();
            int cost = map.PathCost(shortestPath);
            int view = (int)(((double)count / (double)size)*100);
            
            sw.WriteLine(item.Item1 + '\n' + "Cost = " + cost );
            Console.WriteLine("View: " + view + "%");
            for (int i = 0; i<shortestPath.Count();i++)
            {
                sw.Write("(" + shortestPath[i].x + ", " + shortestPath[i].y + "), " +" " );
            }
            sw.WriteLine();
            sw.WriteLine();
            

        }
       


    }

        
}