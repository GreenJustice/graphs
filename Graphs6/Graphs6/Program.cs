using Graphs6;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Graphs6 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            int flag = 0;
            IGraph? graph = null;
            StreamWriter? sw = null;
            int? startVertex = null;
            int? endVertex = null;
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
                if (args[i] == "-m" || args[i] == "-e" || args[i] == "-l")
                {
                    graph = new GraphMatrix(args[i + 1], args[i]);
                }
                else if (args[i] == "-o")
                {
                    sw = new StreamWriter(args[i + 1]);
                    sw.AutoFlush = true;
                }
                else if (args[i] == "-n")
                {
                    startVertex = int.Parse(args[i + 1]);
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
            foreach (var item in graph.AdjacencyMatrix())
            {
                if (item < 0)
                {
                    flag = 1;
                    sw.WriteLine("Graph contains edges with negative weight.");
                    break;
                }
            }
            if (flag == 0)
            {
                sw.WriteLine("Graph does not contain edges with negative weight.");
            }
            foreach (var arg in args)
            {
                if (arg =="-b")
                {
                    var bellmanAlg = graph.BellmanFindShortestPath((int)startVertex);
                    sw.WriteLine("Bellman-Floyd-Mura: ");
                    if (bellmanAlg.Item2 == false)
                    {
                        int k = 0;
                        foreach (var item in bellmanAlg.Item1)
                        {
                            if (k != (int)startVertex)
                            {
                                sw.WriteLine(startVertex + " => " + k + " = " + item);

                            }
                            k++;
                        }
                    }
                    else
                    {
                        sw.WriteLine("the graph does not meet the requirements");
                    }
                }
                if (arg == "-t")
                {
                    var levitAlg = graph.LevitFindShortestPath((int)startVertex);
                    sw.WriteLine("Levit: ");
                    if (levitAlg.Item2 == false)
                    {
                        int k = 0;
                        foreach (var item in levitAlg.Item1)
                        {
                            if (k != (int)startVertex)
                            {
                                sw.WriteLine(startVertex + " => " + k + " = " + item);
                            }
                            k++;
                        }

                    }
                    else
                    {
                        sw.WriteLine("the graph does not meet the requirements");
                    }
                }
                if (arg == "-d" )
                {
                    if (flag == 0)
                        if (startVertex != null) graph.Dijkstra((int)startVertex, sw);
                }
                    
            }
            
            

            void ShowHelp()
            {
                Console.WriteLine("Performed by: Vasilii Siniakov\n Group: М3О-225Бк-21 \n Task №6 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n -n - StartVertex \n -o - The result is output to a file ");
            }
            
        }






    }

}