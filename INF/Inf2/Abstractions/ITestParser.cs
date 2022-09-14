using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Парсер тестов
/// </summary>
public interface ITestParser
{
    /// <summary>
    /// Считать определение теста
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <param name="fileContent">Содержимое файла</param>
    /// <returns>Описание теста</returns>
    public TestDescription ParseDescription(string filePath, string fileContent);

    /// <summary>
    /// Считать тест-кейс
    /// </summary>
    /// <param name="filePath">Путь к файлу</param>
    /// <param name="testDescription">Описание теста</param>
    /// <param name="fileContent">Содержимое файлв</param>
    /// <returns>Тест-кейс</returns>
    public TestCase ParseCase(string filePath, TestDescription testDescription, string fileContent);
}