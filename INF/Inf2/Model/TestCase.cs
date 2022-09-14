namespace Inf2.Model;

/// <summary>
/// Тест-кейс
/// </summary>
/// <param name="Name">Имя кейса</param>
/// <param name="Parameters">Параметры теста</param>
/// <param name="Expected">Ожидания теста</param>
public record TestCase(
    string Name,
    Dictionary<string, object> Parameters,
    object Expected)
{
    public TestCaseResult WithResult(
        TestCaseResults results,
        Exception? ex = null,
        string message = "")
        => new TestCaseResult(this, results, null, message);

    public TestCaseResult Assert(object? actual)
        => Expected.Equals(actual)
            ? WithResult(TestCaseResults.Success)
            : WithResult(TestCaseResults.WrongAnswer, message: $"Ожидалось: {Expected}, получено: {actual}");
};