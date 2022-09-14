namespace Aisd_1.Evaluator;

public class ConsoleExpressionProvider
{
    public string GetExpression(string name)
    {
        Console.WriteLine("Укажите {0}", name);
        return Console.ReadLine();
    }
}