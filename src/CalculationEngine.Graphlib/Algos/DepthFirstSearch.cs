namespace CalculationEngine.Graphlib.Algos;

public static class DepthFirstSearch
{
    // Depth First Traversal (DFS)
    public static List<Vertex<T>> DFS<T>(this Graph<T> graph, int startIndex = 0) where T : class
    {
        var count = graph.Vertices.Count;
        var visited = new bool[count];
        var result = new List<Vertex<T>>(count);

        graph.DFS(graph.Vertices[startIndex], visited, result);

        return result;
    }

    private static void DFS<T>(this Graph<T> graph,
        Vertex<T> vertex,
        bool[] visited,
        List<Vertex<T>> result) where T : class
    {
        result.Add(vertex);
        visited[vertex.Index] = true;

        foreach (var child in vertex.Children)
        {
            if (!visited[child.Index])
            {
                graph.DFS(child, visited, result);
            }
        }
    }
}
