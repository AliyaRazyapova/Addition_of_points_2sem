using Aisd_1.Tokens;

namespace Aisd_1.Nodes;

public abstract record NodeOperatorUnary(NodeBase Operand) : NodeBase
{
    public override IReadOnlyList<NodeBase>? Children { get; } = new[] { Operand };
    
    public abstract TokenTypeOperator Operator { get; }
}