namespace CalculationEngine.Graphlib;
public static class GraphAlgos
{

    #region DFS
    public static List<Vertex<T>> DFS<T>(this Graph<T> graph) where T : class
    {
        bool[] isVisited = new bool[graph.Vertices.Count];

        List<Vertex<T>> result = [];

        graph.DFS(isVisited, graph.Vertices[0], result);

        return result;
    }
    private static void DFS<T>(this Graph<T> graph,
        bool[] isVisited,
        Vertex<T> vertex,
        List<Vertex<T>> result) where T : class
    {
        result.Add(vertex);

        isVisited[vertex.Index] = true;

        foreach (Vertex<T> neighbor in vertex.Neighbors)
        {
            if (!isVisited[neighbor.Index])
            {
                graph.DFS(isVisited, neighbor, result);
            }
        }
    }

    #endregion

    #region BFS
    public static List<Vertex<T>> BFS<T>(this Graph<T> graph) where T : class
    {
        return graph.BFS(graph.Vertices[0]);
    }

    private static List<Vertex<T>> BFS<T>(
        this Graph<T> graph,
        Vertex<T> vertex) where T : class
    {
        bool[] isVisited = new bool[graph.Vertices.Count];
        isVisited[vertex.Index] = true;
        List<Vertex<T>> result = [];
        Queue<Vertex<T>> queue = [];
        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            Vertex<T> next = queue.Dequeue();
            result.Add(next);
            foreach (Vertex<T> neighbor in next.Neighbors)
            {
                if (!isVisited[neighbor.Index])
                {
                    isVisited[neighbor.Index] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return result;
    }
    #endregion
}
