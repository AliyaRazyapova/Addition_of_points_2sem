﻿using Aisd_1.Tokens;

namespace Aisd_1.Nodes;

public record NodeOperatorNegate(NodeBase Operand) : NodeOperatorUnary(Operand)
{
    public override TokenTypeOperator Operator => (TokenTypeOperator)TokenTypes.Minus;

    public override string StringRepresentation => Operand switch
    {
        NodeNumber number => $"-{number}",
        _ => "- Унарный"
    };

    public override string ToString() => $"(-{Operand})";

    public override NodeBase CloneWithChildren(NodeBase[] children)
        => new NodeOperatorNegate(children[0]);
}