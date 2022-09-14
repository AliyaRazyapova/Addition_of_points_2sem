namespace Inf2.Model;

/// <summary>
/// Результат проверки назначения студента
/// </summary>
/// <param name="Student">Студент</param>
/// <param name="Result">Результат проверки</param>
public record StudentAssignmentResult(Student Student, TestResult[] Result)
{
    /// <summary>
    /// Общее количество кейсов
    /// </summary>
    public int TotalCases => Result.Aggregate(0, (acc, r) => acc + r.CaseCount);

    /// <summary>
    /// Количество проверенных кейсов
    /// </summary>
    public int FailedCases => Result.Aggregate(0, (acc, r) => acc + r.FailedCases);
};