using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Модуль сохранения результатов
/// </summary>
public interface IResultsSaver
{
    /// <summary>
    /// Сохранить результаты
    /// </summary>
    /// <param name="results">Результаты тестирования</param>
    public void SaveAll(IEnumerable<StudentAssignmentResult> results);

    /// <summary>
    /// Сохранить результат
    /// </summary>
    /// <param name="result">Результат тестирования</param>
    public void SaveResult(StudentAssignmentResult result);
}