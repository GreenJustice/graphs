using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs11
{
    using Vertex = System.Int32;
    static class KosarajuSCC // Kosaraju Strong Connected Components
    {
        public static void Dfs(IGraph graph, Vertex v, bool[] visited,HashSet<Vertex> newVisited)
        {
            visited[v] = true;
            newVisited.Add(v);
            foreach(var item in graph.AdjacencyList(v))
            {
                if (!visited[item])
                {
                    Dfs(graph,item,visited,newVisited);
                }
            }
        }
        public static void FillStack(IGraph graph, Vertex v, bool[] visited, Stack<Vertex> stack)
        {
            visited[v] = true;
            foreach (var item in graph.AdjacencyList(v))
            {
                if (!visited[item])
                {
                    FillStack(graph, item, visited, stack);
                }
            }
            stack.Push(v);
        }
        public static  List<HashSet<Vertex>> Kosaraju( IGraph graph)
        {
            var result = new List<HashSet<Vertex>>();
            var stack = new Stack<Vertex>();
            var visited = new bool[graph.VertexCount()];
            for (int i = 0; i < visited.Count(); i++)
            {
                if(visited[i]==false)
                {
                    FillStack(graph, i, visited, stack);
                }
            }
            var reversed = ReverseGraph(graph);
            visited = visited.Select(x => false).ToArray();
            while (stack.Count>0)
            {
                var current = stack.Pop();
                if (!visited[current])
                {
                    var newVisited = new HashSet<Vertex>();
                    Dfs(reversed, current, visited, newVisited);
                    result.Add(newVisited);
                }
            }

            return result;
        }
        public static IGraph ReverseGraph(this IGraph graph)
        {
            var matrix = graph.AdjacencyMatrix();
            var reversedMatrix = new int[graph.VertexCount(), graph.VertexCount()];
            for (int i = 0; i < graph.VertexCount(); i++)
                for (int j = 0; j < graph.VertexCount(); j++)
                    reversedMatrix[i, j] = matrix[j, i];
            return new GraphMatrix(reversedMatrix);
                
        }
        
    }
}