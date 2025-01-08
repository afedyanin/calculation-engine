namespace CalculationEngine.Graphlib;
public static class GraphAlgos
{

    #region DFS
    public static List<Vertex<T>> DFS<T>(this Graph<T> graph) where T : class
    {
        bool[] isVisited = new bool[graph.Vertices.Count];

        List<Vertex<T>> result = [];

        graph.DFS(graph.Vertices[0], isVisited, result);

        return result;
    }
    private static void DFS<T>(this Graph<T> graph,
        Vertex<T> vertex,
        bool[] isVisited,
        List<Vertex<T>> result) where T : class
    {
        result.Add(vertex);

        isVisited[vertex.Index] = true;

        foreach (var neighbor in vertex.Neighbors)
        {
            if (!isVisited[neighbor.Index])
            {
                graph.DFS(neighbor, isVisited, result);
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
            var next = queue.Dequeue();
            result.Add(next);
            foreach (var neighbor in next.Neighbors)
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

    #region Cycle Detection

    public static bool HasAnyCycle<T>(this Graph<T> graph) where T : class
    {
        var count = graph.Vertices.Count;
        bool[] isVisited = new bool[count];
        bool[] marked = new bool[count];

        return graph.HasAnyCycle(graph.Vertices[0], isVisited, marked);
    }

    private static bool HasAnyCycle<T>(this Graph<T> graph,
        Vertex<T> vertex,
        bool[] isVisited,
        bool[] marked) where T : class
    {
        isVisited[vertex.Index] = true;
        marked[vertex.Index] = true;

        foreach (var neighbor in vertex.Neighbors)
        {
            if (marked[neighbor.Index])
            {
                return true;
            }
            if (!isVisited[neighbor.Index])
            {
                return graph.HasAnyCycle(neighbor, isVisited, marked);
            }
        }

        marked[vertex.Index] = false;
        return false;
    }

    #endregion
}
