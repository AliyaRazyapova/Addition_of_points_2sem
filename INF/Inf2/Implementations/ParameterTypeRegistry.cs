using Inf2.Abstractions;

namespace Inf2;

/// <inheritdoc />
public class ParameterTypeRegistry : IParameterTypeRegistry
{
    private record TypeAndParser(Type Type, Func<string, object?> Parser);

    private readonly Dictionary<string, TypeAndParser> _types;

    public ParameterTypeRegistry()
    {
        _types = new Dictionary<string, TypeAndParser>();
    }

    /// <inheritdoc />
    public void RegisterType<T>(string name, Func<string, T> parser)
    {
        if(string.IsNullOrEmpty(name))
            throw new ArgumentException("Пустое имя", nameof(name));
        _types.Add(name, new TypeAndParser(typeof(T), x => parser(x)));
    }

    /// <inheritdoc />
    public Type? GetTypeByName(string name)
        => _types.TryGetValue(name, out var value) ? value.Type : null;

    /// <inheritdoc />
    /// <exception cref="ArgumentException"></exception>
    public object? Parse(Type type, string value)
    {
        var typeAndParser = _types.Values.FirstOrDefault(x => x.Type == type) ??
                            throw new ArgumentException($"Неизвестный тип {type}", nameof(type));
        return typeAndParser?.Parser(value);
    }
}