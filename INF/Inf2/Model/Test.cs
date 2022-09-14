namespace Inf2.Model;

/// <summary>
/// Тесты(назначения)
/// </summary>
/// <param name="TestDescription">Описание задания</param>
/// <param name="TestCases">Тестовый кейс</param>
public record Test(TestDescription TestDescription, TestCase[] TestCases)
{
    /// <summary>
    /// Провалить все кейсы с результатом
    /// </summary>
    /// <param name="result">Результат</param>
    /// <param name="message">Сообщение</param>
    /// <returns>Результат</returns>
    public TestResult FailAllWithResult(TestCaseResults result, string message = "")
    {
        return new TestResult(
            this,
            TestCases
                .Select(tc => tc.WithResult(result, message: message))
                .ToArray());
    }
};