namespace Aisd_1.Evaluator;

public class ConsoleDoubleProvider : IVariableValueProvider
{
    private Dictionary<string, double> _cache = new();

    public double GetValue(string name)
    {
        if (!_cache.ContainsKey(name))
        {
            Console.Write("Укажите значение для {0}: ", name);
            _cache[name] = double.Parse(Console.ReadLine());
        }
        return _cache[name];
    }
}