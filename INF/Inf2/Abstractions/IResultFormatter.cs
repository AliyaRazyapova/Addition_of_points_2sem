using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Форматтер результата теста
/// </summary>
public interface IResultFormatter
{
    /// <summary>
    /// Отформатировать результат в строку
    /// </summary>
    /// <param name="result">Результат</param>
    /// <returns>Строковое представление результата</returns>
    public string SerializeResults(StudentAssignmentResult result);
}