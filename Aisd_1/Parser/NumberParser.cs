using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public class NumberParser : IPrefixParser
{
    public NodeBase Parse(Parser parser, Token token)
        => new NodeNumber(double.Parse(token.Value));
}