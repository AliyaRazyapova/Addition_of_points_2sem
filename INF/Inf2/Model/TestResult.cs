namespace Inf2.Model;

/// <summary>
/// Результат тестирования
/// </summary>
/// <param name="Test">Тест</param>
/// <param name="Results">Результат</param>
public record TestResult(Test Test, TestCaseResult[] Results)
{
    /// <summary>
    /// Успешность теста
    /// </summary>
    public bool IsSuccess => Results.All(x => !x.IsFailed);

    /// <summary>
    /// Количество тест-кейсов
    /// </summary>
    public int CaseCount => Results.Length;

    /// <summary>
    /// Количество неудачных тест-кейсов
    /// </summary>
    public int FailedCases => Results.Count(x => x.IsFailed);
};
