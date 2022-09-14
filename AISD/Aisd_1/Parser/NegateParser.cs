using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public class NegateParser : IPrefixParser
{
    public NodeBase Parse(Parser parser, Token token)
        => new NodeOperatorNegate(parser.Parse((int)Priority.Prefix));
    
}