using System.Collections.ObjectModel;

namespace CalcEngine.Client;

// https://stackoverflow.com/questions/66893/tree-data-structure-in-c-sharp
internal class TreeNode<T>
{
    private readonly List<TreeNode<T>> _children;

    public T Value { get; }

    public TreeNode<T>? Parent { get; private set; }

    public TreeNode<T> this[int i]
    {
        get { return _children[i]; }
    }

    public ReadOnlyCollection<TreeNode<T>> Children
    {
        get { return _children.AsReadOnly(); }
    }

    public TreeNode(T value)
    {
        Value = value;
    }

    public TreeNode<T> AddChild(T value)
    {
        var node = new TreeNode<T>(value) { Parent = this };
        _children.Add(node);
        return node;
    }

    public TreeNode<T>[] AddChildren(params T[] values)
    {
        return values.Select(AddChild).ToArray();
    }

    public bool RemoveChild(TreeNode<T> node)
    {
        return _children.Remove(node);
    }

    public void Traverse(Action<T> action)
    {
        action(Value);

        foreach (var child in _children)
        {
            child.Traverse(action);
        }
    }

    public IEnumerable<T> Flatten()
    {
        return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
    }
}
