using Aisd_1.Evaluator;
using Aisd_1.Nodes;
using Aisd_1.Tokens;

namespace Aisd_1.Visitor;

public class ReplaceVarVisitor : IVisitor
{
    private readonly Tokenizer _tokenizer;
    private readonly Parser.Parser _parser;
    private readonly ConsoleExpressionProvider _expressionProvider;

    public ReplaceVarVisitor(
        Tokenizer tokenizer,
        Parser.Parser parser,
        ConsoleExpressionProvider expressionProvider)
    {
        _tokenizer = tokenizer;
        _parser = parser;
        _expressionProvider = expressionProvider;
    }

    public NodeBase Visit(NodeBase nodeBase)
    {
        if (nodeBase is NodeVariable variable)
            return _parser.Parse(
                _tokenizer.Tokenize(
                    _expressionProvider.GetExpression(variable.Name)));

        return nodeBase.CloneWithChildren(
            nodeBase.Children
                .Select(x => Visit(x)).ToArray()
            );
    }
}