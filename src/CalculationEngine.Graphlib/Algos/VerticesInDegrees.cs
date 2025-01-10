namespace CalculationEngine.Graphlib.Algos;
public static class VerticesInDegrees
{
    // Полустепень захода вершины (indegree) - количество дуг, заходящих в эту вершину.
    public static int[] InDegrees<T>(this Graph<T> graph) where T : class
    {
        var inDegreeCount = new int[graph.Vertices.Count];

        foreach (var vertex in graph.Vertices)
        {
            foreach (var item in vertex.Children)
            {
                inDegreeCount[item.Index]++;
            }
        }

        return inDegreeCount;
    }

    public static List<Vertex<T>>[] GetParents<T>(this Graph<T> graph) where T : class
    {
        var parents = new List<Vertex<T>>[graph.Vertices.Count];

        foreach (var vertex in graph.Vertices)
        {
            foreach (var item in vertex.Children)
            {
                var parentsList = parents[item.Index];

                if (parentsList == null)
                {
                    parents[item.Index] = [];
                    parentsList = parents[item.Index];
                }
                parentsList.Add(vertex);
            }
        }

        return parents;
    }
}
