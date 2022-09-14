using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Исполнитель тестов
/// </summary>
public interface ITestRunner
{
    /// <summary>
    /// Проверить назначение студента
    /// </summary>
    /// <param name="studentAssignment">Назначение студента</param>
    /// <returns>Результат проверки</returns>
    StudentAssignmentResult Check(StudentAssignment studentAssignment);
}