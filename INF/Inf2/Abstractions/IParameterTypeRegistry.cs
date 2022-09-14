namespace Inf2.Abstractions;

/// <summary>
/// Репозиторий для типов параметров
/// </summary>
public interface IParameterTypeRegistry
{
    /// <summary>
    /// Зарегестрировать тип
    /// </summary>
    /// <param name="name">Идентификатор типа</param>
    /// <param name="parser">Функция парсер типа</param>
    /// <typeparam name="T">.Net тип</typeparam>
    void RegisterType<T>(string name, Func<string, T> parser);

    /// <summary>
    /// Получить тип параметра по его идентификатору
    /// </summary>
    /// <param name="name">идентификатор</param>
    /// <returns>Тип</returns>
    Type? GetTypeByName(string name);

    /// <summary>
    /// Распарсить объект по его типу
    /// </summary>
    /// <param name="type">Тип</param>
    /// <param name="value">Значение</param>
    /// <returns>Объкет</returns>
    object? Parse(Type type, string value);
}