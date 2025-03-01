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

            int i = vFrom.Children.IndexOf(vTo);
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
        from.Children.Add(to);
    }

    public bool RemoveEdge(Vertex<T> from, Vertex<T> to)
    {
        int index = from.Children.FindIndex(n => n == to);
        if (index < 0)
        {
            return false;
        }

        from.Children.RemoveAt(index);
        return true;
    }

    public List<Edge<T>> GetEdges()
    {
        List<Edge<T>> edges = [];

        foreach (Vertex<T> from in Vertices)
        {
            for (int i = 0; i < from.Children.Count; i++)
            {
                Edge<T> edge = new()
                {
                    From = from,
                    To = from.Children[i],
                };
                edges.Add(edge);
            }
        }
        return edges;
    }

    private void UpdateIndices()
    {
        int i = 0;
        Vertices.ForEach(n => n.Index = i++);
    }
}
