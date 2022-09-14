namespace Inf2.Implementations;

/// <summary>
/// Фабрика реестра типов
/// </summary>
public class ParameterTypeRegistryFactory
{
    /// <summary>
    /// Создает реестр типов параметров по условию:
    /// В качестве типов могут выступать только следующие типы: int, double, string, char, bool
    /// </summary>
    /// <returns>Реестр с типами по умолчанию</returns>
    public static ParameterTypeRegistry CreateDefault()
    {
        var registry = new ParameterTypeRegistry();

        registry.RegisterType("int", int.Parse);
        registry.RegisterType("double", double.Parse);
        registry.RegisterType("string", s => s);
        registry.RegisterType("char", char.Parse);
        registry.RegisterType("bool", bool.Parse);

        return registry;
    }
}