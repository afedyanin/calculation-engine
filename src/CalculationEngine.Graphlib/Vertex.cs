namespace CalculationEngine.Graphlib;

public class Vertex<T> where T : class
{
    public int Index { get; set; }

    public required T Value { get; set; }

    public List<Vertex<T>> Neighbors { get; set; } = [];

    public override string ToString() => $"Index={Index} Value={Value}";
}
