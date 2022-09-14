using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public class BinaryOperatorParser: IInfixParser
{
    private readonly Func<NodeBase, NodeBase, NodeBase> _makeNode;
    public Priority Priority { get; }

    public BinaryOperatorParser(Priority priority, Func<NodeBase, NodeBase, NodeBase> makeNode)
    {
        _makeNode = makeNode;
        Priority = priority;
    }

    public NodeBase Parse(Parser parser, NodeBase left, Token token) 
        => _makeNode(left, parser.Parse((int)Priority));
}