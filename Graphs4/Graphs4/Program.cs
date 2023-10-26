using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Graphs4
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            int flag = 0;
            IGraph? graph = null;
            StreamWriter? sw = null;

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
                    graph = new GraphMatrix(args[i + 1]);


                }
                else if (args[i] == "-e")
                {
                    graph = new EdgeList(args[i + 1]);

                }
                else if (args[i] == "-l")
                {
                    graph = new AdjacencyGraph(args[i + 1]);
                }
                else if (args[i] == "-o")
                {
                    sw = new StreamWriter(args[i + 1]);
                    sw.AutoFlush = true;
                }

            }
            if (graph == null)
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
            foreach (var arg in args)
            {
                if (arg == "-p")
                {
                    var mst = graph.PrimMST();
                    int mincost = 0;
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mst)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincost += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincost);
                    flag = 1;
                    break;
                }
                else if (arg == "-k")
                {
                    var mst = graph.KruskalMst();
                    int mincost = 0;
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mst)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincost += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincost);
                    flag = 1;
                    break;
                }
                else if (arg == "-b")
                {
                    var mst = graph.BoruvkaMST();
                    int mincost = 0;
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mst)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincost += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincost);
                    flag = 1;
                    break;

                }
                else if (arg == "-s")
                {
                    sw.WriteLine("PRIM'S ALGORITHM");
                    Stopwatch stopwatch = new Stopwatch();
                    stopwatch.Start();
                    var mstPrim = graph.PrimMST();
                    stopwatch.Stop();
                    int mincostPrim = 0;
              
                   
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mstPrim)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincostPrim += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincostPrim);
                    sw.WriteLine("The algorithm worked " + stopwatch.ElapsedMilliseconds + " Milliseconds");
                    sw.WriteLine("KRUSKAL'S ALGORITHM");
                    stopwatch.Restart();
                    var mstKrusk = graph.KruskalMst();
                    stopwatch.Stop();
                    int mincostKrusk = 0;
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mstKrusk)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincostKrusk += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincostKrusk);
                    
                    sw.WriteLine("The algorithm worked " + stopwatch.ElapsedMilliseconds + " Milliseconds" );
                    sw.WriteLine("BORUVKA'S ALGORITHM");
                    stopwatch.Restart();
                    var mstBoruvka = graph.BoruvkaMST();
                    stopwatch.Stop();
                    int mincostBoruvka = 0;
                    sw.WriteLine("Minimum spanning tree:");
                    sw.Write("[");
                    foreach (var i in mstBoruvka)
                    {
                        sw.Write($"({i.Item1} {i.Item2} {i.Item3}) ");
                        mincostBoruvka += i.Item3;
                    }
                    sw.WriteLine("]");
                    sw.WriteLine("Weight of spanning tree: " + mincostBoruvka);
                    
                    sw.WriteLine("The algorithm worked " + stopwatch.ElapsedMilliseconds + " Milliseconds");
                    flag = 1;
                    break;
                }
                
            }
            if (flag==0)
            {
                throw new Exception("Input correct key");
            }

            void ShowHelp()
            {
                Console.WriteLine("Performed by: Siniakov Vasilii\n Group: М3О-225Бк-21 \n Task №4 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n -o - The result is output to a file \n -p - Implementation of the Prima's algorithm \n -k - Implementation of the Kruskal's algorithm \n -b - Implementation of the Boruvka's algorithm");
            }
        
    }






    }

}