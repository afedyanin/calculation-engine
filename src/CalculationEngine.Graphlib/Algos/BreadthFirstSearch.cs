namespace CalculationEngine.Graphlib.Algos;
public static class BreadthFirstSearch
{
    // Breadth First Search (BFS)
    public static List<Vertex<T>> BFS<T>(this Graph<T> graph) where T : class
    {
        return graph.BFS(graph.Vertices[0]);
    }

    private static List<Vertex<T>> BFS<T>(
        this Graph<T> graph,
        Vertex<T> vertex) where T : class
    {
        var count = graph.Vertices.Count;
        var visited = new bool[count];
        var queue = new Queue<Vertex<T>>(count);
        var result = new List<Vertex<T>>(count);

        visited[vertex.Index] = true;
        queue.Enqueue(vertex);

        while (queue.Count > 0)
        {
            var next = queue.Dequeue();
            result.Add(next);
            foreach (var child in next.Children)
            {
                if (!visited[child.Index])
                {
                    visited[child.Index] = true;
                    queue.Enqueue(child);
                }
            }
        }

        return result;
    }
}
