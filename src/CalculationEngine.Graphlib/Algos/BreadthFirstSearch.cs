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
            foreach (var neighbor in next.Neighbors)
            {
                if (!visited[neighbor.Index])
                {
                    visited[neighbor.Index] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }

        return result;
    }
}
