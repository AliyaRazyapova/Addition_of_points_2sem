using Aisd_1.Utils;

namespace Aisd_1.Tokens;

public class TokenTypeVariable : TokenType
{
    public override bool Matches(string lexeme)
    {
        return lexeme.IsVariable();
    }

    public override int Priority => 6;
}