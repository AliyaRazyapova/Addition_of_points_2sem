using System.Diagnostics;
using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Воркер проверки заданий студентов
/// </summary>
public interface ICheckerWorker
{
    /// <summary>
    /// Проверить задания студентоы
    /// </summary>
    /// <param name="assignments">Набор заданий</param>
    /// <returns>Результаты тестирования</returns>
    StudentAssignmentResult[] Process(StudentAssignment[] assignments);
}