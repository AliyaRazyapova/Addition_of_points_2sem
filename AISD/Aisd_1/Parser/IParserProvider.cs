using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public interface IParserProvider
{
    IInfixParser? GetInfixParser(TokenType tokenType);
    
    IPrefixParser? GetPrefixParser(TokenType tokenType);
}