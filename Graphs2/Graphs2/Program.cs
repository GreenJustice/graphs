using System;
using System.ComponentModel;


namespace Graphs2 // Note: actual namespace depends on the project name.
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
            var connectComponent = graph.IsConnectNotOriented();
            if (connectComponent.Count == 1)
                sw.WriteLine("graph is connected.");
            else sw.WriteLine($"Graph is not connected and contains {connectComponent.Count()} connected components.");
            sw.WriteLine("Connected components:");
            for (int i = 0; i < connectComponent.Count(); i++)
            {
                foreach (var next in connectComponent[i])
                {
                    sw.Write(next + " ");
                }
                sw.WriteLine();
            }
            if (graph.IsDirected()==true)
            {
                
                var strongly = graph.Kosaraju();
                if (strongly.Count() == 1)
                    sw.WriteLine("Digraph is strongly connected.");
                else sw.WriteLine($"Digraph is weakly connected and contains {strongly.Count()} strongly connected components.");


                sw.WriteLine("Strongly connected components:");
                for (int i = 0; i < strongly.Count(); i++)
                {
                    foreach (var next in strongly[i])
                    {
                        sw.Write(next + " ");
                    }
                    sw.WriteLine();
                }
            }
            
            void ShowHelp()
            {
                Console.WriteLine("Performed by: Siniakov Vasilii\n Group: М3О-225Бк-21 \n Task №4 \n Keys: \n -m - The graph is read from the adjacency matrix \n -e - The graph is read from the list of edges \n -l - The graph is read from the adjacency list \n -o - The result is output to a file \n ");
            }
        }
    }

}