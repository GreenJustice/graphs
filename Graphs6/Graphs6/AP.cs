using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphs6
{
    static public class AP
    {
        static private HashSet<int> ap= new HashSet<int>();
        static int time = 0;
        

        static void APUtil
            (
                IGraph graph,
                int u, 
                bool[] visited, 
                int[] disc,
                int[] low, 
                int[] parent
            )
        {

            // Count of children in DFS Tree
            int children = 0;

            // Mark the current node as visited
            visited[u] = true;

            // Initialize discovery time and low value
            disc[u] = low[u] = ++time;

            // Go through all vertices adjacent to this
            foreach (int i in graph.AdjacencyList(u))
            {
                int v = i; // v is current adjacent of u

                // If v is not visited yet, then make it a child of u
                // in DFS tree and recur for it
                if (!visited[v])
                {
                    children++;
                    parent[v] = u;
                    APUtil(graph, v, visited, disc, low, parent);

                    // Check if the subtree rooted with v has
                    // a connection to one of the ancestors of u
                    low[u] = Math.Min(low[u], low[v]);

                    // u is an articulation point in following cases

                    // (1) u is root of DFS tree and has two or more children.
                    if (parent[u] == -1 && children > 1)
                        ap.Add(u);

                    // (2) If u is not root and low value of one of its child
                    // is more than discovery value of u.
                    if (parent[u] != -1 && low[v] >= disc[u])
                        ap.Add(u);
                }

                // Update low value of u for parent function calls.
                else if (v != parent[u])
                    low[u] = Math.Min(low[u], disc[v]);
            }
        }

        // The function to do DFS traversal.
        // It uses recursive function APUtil()
        public static HashSet<int> GetAP(this IGraph graph)
        {
            // Mark all the vertices as not visited
            var visited = new bool[graph.VertexCount()];
            var disc = new int[graph.VertexCount()];
            var low = new int[graph.VertexCount()];
            var parent = new int[graph.VertexCount()];
            

            // Initialize parent and visited, and
            // ap(articulation point) arrays
            for (int i = 0; i < graph.VertexCount(); i++)
            {
                parent[i] = -1;
                visited[i] = false;
                ap.Clear();
            }

            // Call the recursive helper function to find articulation
            // points in DFS tree rooted with vertex 'i'
            for (int i = 0; i < graph.VertexCount(); i++)
                if (visited[i] == false)
                    APUtil(graph, i, visited, disc, low, parent);

            // Now ap[] contains articulation points, print them
    
            return ap;
        }
    }
}
