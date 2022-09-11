namespace Aisd_1.Tokens;

public static class TokenRegistry
{
    public static Dictionary<TokenNames, TokenType> Registry { get; } = new()
    {
        [TokenNames.Variable] = new TokenTypeVariable(),
        [TokenNames.Number] = new TokenTypeNumber(),
        [TokenNames.BraceOpen] = new TokenTypeBrace(Side.Open),
        [TokenNames.BraceClose] = new TokenTypeBrace(Side.Close),
        [TokenNames.Plus] = new TokenTypeOperatorAdd(),
        [TokenNames.Minus] = new TokenTypeOperatorMinus(),
        [TokenNames.Multiply] = new TokenTypeOperatorMultiply(),
    };

    public static List<TokenType> Tokens { get; }

    static TokenRegistry()
    {
        Tokens = Registry.Values.OrderByDescending(x => x.Priority).ToList();
    }
}