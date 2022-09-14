namespace Aisd_1.Tokens;

public static class TokenTypes
{
    public static TokenType Number => TokenRegistry.Registry[TokenNames.Number];
    public static TokenType Plus => TokenRegistry.Registry[TokenNames.Plus];
    public static TokenType Minus => TokenRegistry.Registry[TokenNames.Minus];
    public static TokenType Multiply => TokenRegistry.Registry[TokenNames.Multiply];
    public static TokenType Variable => TokenRegistry.Registry[TokenNames.Variable];
    public static TokenType BraceOpen => TokenRegistry.Registry[TokenNames.BraceOpen];
    public static TokenType BraceClose => TokenRegistry.Registry[TokenNames.BraceClose];
}