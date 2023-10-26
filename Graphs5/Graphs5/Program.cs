using Graphs5;
using System;
using System.ComponentModel;
using System.Diagnostics;

namespace Graphs5 // Note: actual namespace depends on the project name.
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
                if (args[i] == "-m"|| args[i] == "-e"|| args[i] == "-l")
                {
                    graph = new GraphMatrix(args[i + 1], args[i]);
                }
                else if (args[i] == "-o")
                {
                    sw = new StreamWriter(args[i + 1]);
                    sw.AutoFlush = true;
                }
                else if (args[i]=="-n")
                {
                    startVertex = int.Parse(args[i + 1]);
                }
                else if (args[i] == "-d")
                {
                    endVertex = int.Parse(args[i + 1]);
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
            
            if (startVertex != null && endVertex != null) graph.Dijkstra((int)startVertex,(int)endVertex, sw);
            

            void ShowHelp()
            {
                Console.WriteLine("Performed by: Siniakov Vasilii\n Group: М3О-225Бк-21 \n Task №5 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n -n - StartVertex for Dijkstra  \n -d - EndVertex for Dijkstra  \n -o - The result is output to a file ");
            }
            
        }






    }

}