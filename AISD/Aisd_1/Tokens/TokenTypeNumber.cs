using Aisd_1.Utils;

namespace Aisd_1.Tokens;

public class TokenTypeNumber : TokenType
{
    public override bool Matches(string lexeme) 
        => lexeme[0].IsNumeric() && double.TryParse(lexeme, out _);

    public override int Priority => 5;
}