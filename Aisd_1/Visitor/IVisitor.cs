using Aisd_1.Nodes;

namespace Aisd_1.Visitor;

public interface IVisitor
{
    public NodeBase Visit(NodeBase nodeBase);
}