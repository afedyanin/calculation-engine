namespace CalculationEngine.Graphlib;
public static class GraphAlgos
{

    #region Depth First Traversal (DFS)
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

    #region Breadth First Search (BFS)
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
    // Function to detect cycle in a directed graph
    // https://www.geeksforgeeks.org/detect-cycle-in-a-graph/
    public static bool HasAnyCycle<T>(this Graph<T> graph) where T : class
    {
        var count = graph.Vertices.Count;
        bool[] visited = new bool[count];
        bool[] recStack = new bool[count];

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
            foreach (var neighbor in vertex.Neighbors)
            {
                if (!visited[neighbor.Index] &&
                    graph.HasAnyCycle(neighbor, visited, recStack))
                {
                    return true;
                }
                else if (recStack[neighbor.Index])
                {
                    return true;
                }
            }
        }
        // Remove the vertex from recursion stack
        recStack[vertex.Index] = false;
        return false;
    }

    #endregion
}
