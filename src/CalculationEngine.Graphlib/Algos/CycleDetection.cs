namespace CalculationEngine.Graphlib.Algos;
public static class CycleDetection
{
    // Function to detect cycle in a directed graph
    // https://www.geeksforgeeks.org/detect-cycle-in-a-graph/
    public static bool HasAnyCycle<T>(this Graph<T> graph) where T : class
    {
        var count = graph.Vertices.Count;
        var visited = new bool[count];
        var recStack = new bool[count];

        // Call the recursive helper function to 
        // detect cycle in different DFS trees
        for (var index = 0; index < count; index++)
        {
            if (!visited[index])
            {
                if (graph.HasAnyCycle(graph.Vertices[index], visited, recStack))
                {
                    return true;
                }
            }
        }

        return false;
    }

    // Utility function to detect cycle in a directed graph
    private static bool HasAnyCycle<T>(this Graph<T> graph,
        Vertex<T> vertex,
        bool[] visited,
        bool[] recStack) where T : class
    {
        if (!visited[vertex.Index])
        {
            // Mark the current node as visited 
            // and part of recursion stack
            visited[vertex.Index] = true;
            recStack[vertex.Index] = true;

            // Recur for all the vertices adjacent to this vertex
            foreach (var child in vertex.Children)
            {
                if (!visited[child.Index] &&
                    graph.HasAnyCycle(child, visited, recStack))
                {
                    return true;
                }
                else if (recStack[child.Index])
                {
                    return true;
                }
            }
        }

        // Remove the vertex from recursion stack
        recStack[vertex.Index] = false;
        return false;
    }
}
