using Graphs10;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Graphs10 // Note: actual namespace depends on the project name.
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
            var graphMatrix = graph.AdjacencyMatrix();
            int flag1 = 0, flag2 = 0;
            int source = 0, sink=0;
            for (int i = 0; i < graphMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < graphMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (graphMatrix[i,j]!=0)
                    {
                            flag1 = 1;
                    }
                    if (graphMatrix[j,i]!=0)
                    {
                            flag2 = 1;
                    }
                }
                if (flag1 == 0)
                {
                    sink = i;
                }
                if (flag2 == 0)
                {
                    source = i;
                }
                flag1 = 0; flag2 = 0;
            }
            var result = graph.FordFulkerson(source,sink);
            var resultMatrix = result.Item2;
            
            Console.WriteLine($"maximum flow from {source + 1} in {sink + 1}: " + result.Item1) ;
            for (int i = 0; i < resultMatrix.GetUpperBound(0) + 1; i++)
            {
                for (int j = 0; j < resultMatrix.GetUpperBound(0) + 1; j++)
                {
                    if (graphMatrix[i,j]!=0)
                    {
                        Console.WriteLine($"{i+1} {j+1} {resultMatrix[j, i]}/{graphMatrix[i, j]} ");
                    }
                }
            }


        }

        static void ShowHelp()
        {
            Console.WriteLine("Performed by: Vasilii Siniakov\n Group: М3О-225Бк-21 \n Task №7 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n ");
        }




    }

}