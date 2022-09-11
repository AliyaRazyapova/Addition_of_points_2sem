namespace Aisd_1.Nodes;

public record NodeVariable(string Name) : NodeBase
{
    public override IReadOnlyList<NodeBase>? Children { get; } = new List<NodeBase>(0);
    
    public override string StringRepresentation => Name;

    public override string ToString() => Name;

    public override NodeBase CloneWithChildren(NodeBase[] children)
     => new NodeVariable(Name);
}