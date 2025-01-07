namespace CalculationEngine.Graphlib;

public class Graph<T> where T : class
{
    public List<Vertex<T>> Vertices { get; set; } = [];

    public Edge<T>? this[int from, int to]
    {
        get
        {
            Vertex<T> vFrom = Vertices[from];
            Vertex<T> vTo = Vertices[to];

            int i = vFrom.Neighbors.IndexOf(vTo);
            if (i < 0)
            {
                return null;
            }

            Edge<T> edge = new()
            {
                From = vFrom,
                To = vTo,
            };

            return edge;
        }
    }

    public Vertex<T> AddVertex(T value)
    {
        Vertex<T> node = new() { Value = value };
        Vertices.Add(node);
        UpdateIndices();
        return node;
    }

    public void RemoveVertex(Vertex<T> vertexToRemove)
    {
        Vertices.Remove(vertexToRemove);
        UpdateIndices();
        Vertices.ForEach(n => RemoveEdge(n, vertexToRemove));
    }

    public void AddEdge(Vertex<T> from, Vertex<T> to)
    {
        from.Neighbors.Add(to);
    }

    public void RemoveEdge(Vertex<T> from, Vertex<T> to)
    {
        int index = from.Neighbors.FindIndex(n => n == to);
        if (index < 0)
        {
            return;
        }

        from.Neighbors.RemoveAt(index);
    }

    public List<Edge<T>> GetEdges()
    {
        List<Edge<T>> edges = [];

        foreach (Vertex<T> from in Vertices)
        {
            for (int i = 0; i < from.Neighbors.Count; i++)
            {
                Edge<T> edge = new()
                {
                    From = from,
                    To = from.Neighbors[i],
                };
                edges.Add(edge);
            }
        }
        return edges;
    }

    public List<Vertex<T>> DFS()
    {
        bool[] isVisited = new bool[Vertices.Count];

        List<Vertex<T>> result = [];

        DFS(isVisited, Vertices[0], result);

        return result;
    }

    public List<Vertex<T>> BFS() => BFS(Vertices[0]);

    private void DFS(bool[] isVisited, Vertex<T> vertex, List<Vertex<T>> result)
    {
        result.Add(vertex);

        isVisited[vertex.Index] = true;

        foreach (Vertex<T> neighbor in vertex.Neighbors)
        {
            if (!isVisited[neighbor.Index])
            {
                DFS(isVisited, neighbor, result);
            }
        }
    }

    private List<Vertex<T>> BFS(Vertex<T> vertex)
    {
        bool[] isVisited = new bool[Vertices.Count];
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

    private void UpdateIndices()
    {
        int i = 0;
        Vertices.ForEach(n => n.Index = i++);
    }
}
