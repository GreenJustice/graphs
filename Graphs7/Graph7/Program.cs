using Graph7;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Graph7 // Note: actual namespace depends on the project name.
{
    public class Program
    {
        static void Main(string[] args)
        {
            int flag = 0;
            IGraph? graph = null;
            StreamWriter? sw = null;
            int? startVertex = null;
            
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
            try
            {
                var dist = graph.Johnson();
                for (int i = 0; i < dist.Count; i++)
                {
                    for (int j = 0; j < dist[i].Count; j++)
                    {
                        if (dist[i][j] < 9000)
                        {
                            if (i != j)
                                Console.WriteLine(i + " - " + j + " : " + dist[i][j] + " ");
                        }
                    }
                    Console.WriteLine();
                }
            }
            catch(Exception e) 
            {
                sw.WriteLine(e.Message);
            }
            

        }

        static void ShowHelp()
        {
            Console.WriteLine("Performed by: Vasilii Sinyakov\n Group: М3О-225Бк-21 \n Task №7 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n ");
        }




    }

}