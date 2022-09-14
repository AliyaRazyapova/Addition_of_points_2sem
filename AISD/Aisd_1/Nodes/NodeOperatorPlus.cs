﻿using Aisd_1.Tokens;

namespace Aisd_1.Nodes;

public record NodeOperatorPlus(NodeBase Left, NodeBase Right) : NodeOperatorBinary(Left, Right)
{
    public override TokenTypeOperator Operator => (TokenTypeOperator)TokenTypes.Plus;
    
    public override string StringRepresentation => "+";

    public override string ToString() => $"({Left} + {Right})";

    public override NodeBase CloneWithChildren(NodeBase[] children) 
        => new NodeOperatorPlus(children[0], children[1]);
}