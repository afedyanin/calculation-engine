namespace CalculationEngine.Graphlib;

public class Edge<T> where T : class
{
    public required Vertex<T> From { get; set; }
    public required Vertex<T> To { get; set; }

    public override string ToString() => $"{From.Index} -> {To.Index}";
}
