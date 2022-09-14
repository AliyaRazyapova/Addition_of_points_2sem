using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Parser;

public class Parser
{
    private TokenPipe _tokenPipe;
    private readonly IParserProvider _parserProvider;

    private readonly List<Token> _tokenCache = new();

    public Parser(TokenPipe pipe, IParserProvider parserProvider)
    {
        _tokenPipe = pipe;
        _parserProvider = parserProvider;
    }

    public Parser(IParserProvider parserProvider)
    {
        _parserProvider = parserProvider;
        _tokenPipe = new TokenPipe();
    }

    public NodeBase Parse(int priority = 0)
    {
        var token = TakeToken();
        var node = _parserProvider
                       .GetPrefixParser(token.Type)
                       ?.Parse(this, token)
                   ?? throw new InvalidOperationException("Выражение не может начинаться с инфиксного оператора");

        while (priority < GetPrecedence())
        {
            token = TakeToken()
                    ?? throw new InvalidOperationException("Выражение неполное");

            node = (_parserProvider.GetInfixParser(token.Type)
                    ?? throw new InvalidOperationException($"Нет парсера для токена {token.Type}")
                ).Parse(this, node, token);
        }

        return node;
    }

    public NodeBase Parse(TokenPipe pipe)
    {
        _tokenPipe = pipe;
        return Parse();
    }

    public int GetPrecedence()
    {
        var token = LookAhead();
        return (int)(token == null
            ? 0
            : _parserProvider.GetInfixParser(token.Type)?.Priority
              ?? 0);
    }

    public Token TakeToken(TokenType expectedType)
        => LookAhead()?.Type != expectedType
            ? throw new InvalidOperationException($"Неожиданный токен {expectedType}, встречен {LookAhead()?.Type}")
            : TakeToken();

    public bool Match(TokenType ex)
    {
        if (LookAhead()?.Type == ex)
        {
            TakeToken();
            return true;
        }

        return false;
    }

    public Token TakeToken()
    {
        LookAhead();

        var res = _tokenCache[0];
        _tokenCache.RemoveAt(0);
        return res;
    }

    public Token? LookAhead(int fetchCount = 0)
    {
        while (fetchCount >= _tokenCache.Count)
        {
            var next = _tokenPipe.Next();

            if (next == null)
                return null;

            _tokenCache.Add(next);
        }

        return _tokenCache[fetchCount];
    }
}