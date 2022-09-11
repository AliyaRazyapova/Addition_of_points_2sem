using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public interface IInfixParser
{
    public Priority Priority { get; }
    
    NodeBase Parse(Parser parser, NodeBase left, Token token);
}