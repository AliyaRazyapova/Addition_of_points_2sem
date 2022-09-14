using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public interface IPrefixParser
{
    NodeBase Parse(Parser parser, Token token);
}