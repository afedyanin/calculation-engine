namespace CalculationEngine.Graphlib.Algos;

public static class DepthFirstSearch
{
    // Depth First Traversal (DFS)
    public static List<Vertex<T>> DFS<T>(this Graph<T> graph) where T : class
    {
        var count = graph.Vertices.Count;
        var visited = new bool[count];
        var result = new List<Vertex<T>>(count);

        graph.DFS(graph.Vertices[0], visited, result);

        return result;
    }

    private static void DFS<T>(this Graph<T> graph,
        Vertex<T> vertex,
        bool[] visited,
        List<Vertex<T>> result) where T : class
    {
        result.Add(vertex);
        visited[vertex.Index] = true;

        foreach (var neighbor in vertex.Neighbors)
        {
            if (!visited[neighbor.Index])
            {
                graph.DFS(neighbor, visited, result);
            }
        }
    }
}
