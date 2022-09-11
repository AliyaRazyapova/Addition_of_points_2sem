using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public class BraceParser : IPrefixParser
{
    public NodeBase Parse(Parser parser, Token token)
    {
        var node = parser.Parse();
        parser.TakeToken(TokenTypes.BraceClose);
        return node;
    }
}