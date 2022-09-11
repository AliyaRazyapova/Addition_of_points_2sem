using Aisd_1.Nodes;

namespace Aisd_1.Evaluator;

public class Evaluator
{
    private readonly IVariableValueProvider _provider;

    public Evaluator(IVariableValueProvider provider)
    {
        _provider = provider;
    }

    public double Evaluate(NodeBase nodeBase)
    {
        return nodeBase switch
        {
            NodeNumber number => number.Value,
            NodeVariable variable => _provider.GetValue(variable.Name),
            NodeOperatorPlus addition => Evaluate(addition.Left) + Evaluate(addition.Right),
            NodeOperatorMinus subtraction => Evaluate(subtraction.Left) - Evaluate(subtraction.Right),
            NodeOperatorMultiply multiplication => Evaluate(multiplication.Left) * Evaluate(multiplication.Right),
            NodeOperatorNegate nodeOperatorNegate => -Evaluate(nodeOperatorNegate.Operand),
            _ => throw new ArgumentOutOfRangeException(nameof(nodeBase), nodeBase, null)
        };
    }
}