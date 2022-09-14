using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Список тестов
/// </summary>
public interface ITestRegistry
{
    /// <summary>
    /// Тесты
    /// </summary>
    IReadOnlyList<Test> LoadedTests { get; }

    /// <summary>
    /// Получить тест по имени
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    Test? GetByName(string name);

    /// <summary>
    /// Загрузить тесты
    /// </summary>
    void LoadTests();
}