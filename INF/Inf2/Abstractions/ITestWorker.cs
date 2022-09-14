using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Воркер для вычисления результата
/// </summary>
public interface ITestWorker
{
    /// <summary>
    /// Провести тест для все
    /// </summary>
    /// <param name="assignments"></param>
    /// <returns></returns>
    StudentAssignmentResult[] Compute(StudentAssignment[] assignments);
}