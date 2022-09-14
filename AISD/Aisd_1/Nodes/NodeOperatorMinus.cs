using Aisd_1.Tokens;

namespace Aisd_1.Nodes;

public record NodeOperatorMinus(NodeBase Left, NodeBase Right) : NodeOperatorBinary(Left, Right)
{
    public override TokenTypeOperator Operator => (TokenTypeOperator)TokenTypes.Minus;
    
    public override string StringRepresentation => "-";

    public override string ToString() => $"({Left} - {Right})";

    public override NodeBase CloneWithChildren(NodeBase[] children) 
        => new NodeOperatorMinus(children[0], children[1]);
}