namespace CalculationEngine.Graphlib.Algos;

public static class TopologicalSorting
{
    // Function to perform Topological Sort
    // https://www.geeksforgeeks.org/topological-sorting
    public static List<Vertex<T>> TopologicalSort<T>(this Graph<T> graph) where T : class
    {
        var count = graph.Vertices.Count;
        var visited = new bool[count];
        var stack = new Stack<Vertex<T>>(count);
        var result = new List<Vertex<T>>(count);

        // Call the recursive helper function to store
        // Topological Sort starting from all vertices one by one
        for (int index = 0; index < count; index++)
        {
            if (!visited[index])
            {
                graph.TopologicalSort(graph.Vertices[index], visited, stack);
            }
        }

        while (stack.Count > 0)
        {
            result.Add(stack.Pop());
        }

        return result;
    }

    private static void TopologicalSort<T>(
        this Graph<T> graph,
        Vertex<T> vertex,
        bool[] visited,
        Stack<Vertex<T>> stack) where T : class
    {
        // Mark the current node as visited
        visited[vertex.Index] = true;

        // Recur for all adjacent vertices
        foreach (var neighbor in vertex.Neighbors)
        {
            if (!visited[neighbor.Index])
            {
                graph.TopologicalSort(neighbor, visited, stack);
            }
        }

        // Push current vertex to stack which stores the result
        stack.Push(vertex);
    }
}
