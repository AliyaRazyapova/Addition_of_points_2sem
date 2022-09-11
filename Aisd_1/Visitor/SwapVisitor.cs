using Aisd_1.Nodes;

namespace Aisd_1.Visitor;

public class SwapVisitor : IVisitor
{
    public NodeBase Visit(NodeBase nodeBase)
        => nodeBase switch
        {
            NodeOperatorPlus nodeOperatorPlus => new NodeOperatorMinus(
                Visit(nodeOperatorPlus.Left),
                Visit(nodeOperatorPlus.Right)),
            NodeOperatorMinus nodeOperatorMinus => new NodeOperatorMultiply(
                Visit(nodeOperatorMinus.Left), 
                Visit(nodeOperatorMinus.Right)),
            NodeOperatorMultiply nodeOperatorMultiply => new NodeOperatorPlus(
                Visit(nodeOperatorMultiply.Left),
                Visit(nodeOperatorMultiply.Right)),
            _ => nodeBase,
        };
}