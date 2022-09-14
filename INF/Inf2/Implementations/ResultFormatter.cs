using System.Text;
using Inf2.Abstractions;
using Inf2.Model;
using Inf2.Utils;

namespace Inf2.Implementations;

/// <inheritdoc />
public class ResultFormatter : IResultFormatter
{
    /// <inheritdoc />
    public string SerializeResults(StudentAssignmentResult result)
    {
        var sb = new StringBuilder();
        sb.Append("Студент: ")
            .AppendLine(result.Student.Name)
            .AppendFormat(
                "Результат: {0}/{1} ({2}/{3} тестов)\n\n",
                result.Result.Count(x => x.IsSuccess),
                result.Result.Length,
                result.TotalCases - result.FailedCases,
                result.TotalCases);

        foreach (var testResult in result.Result)
        {
            SerializeTestResult(sb, testResult);
        }

        return sb.ToString();
    }

    /// <inheritdoc />
    private void SerializeTestResult(StringBuilder sb, TestResult testRes)
    {
        sb.AppendFormat(
            "Упражнение: {0} ({1}). Кейсы: {2}/{3}\n",
            testRes.Test.TestDescription.TaskName,
            testRes.Test.TestDescription.FileName,
            testRes.Results.Count(x => !x.IsFailed),
            testRes.Results.Length);

        foreach (var caseResult in testRes.Results)
        {
            SerializeTestCase(sb, caseResult);
        }

        sb.AppendLine();
    }

    private void SerializeTestCase(StringBuilder sb, TestCaseResult testCaseResult)
    {
        sb.Append(testCaseResult.IsFailed ? "❌" : "✅")
            .AppendFormat(
                "   {0}: {1} {2} ",
                testCaseResult.TestCase.Name,
                testCaseResult.CaseResult.GetDisplayName(),
                testCaseResult.Message)
            .AppendLine(testCaseResult.Ex != null ? testCaseResult.Ex.ToString() : string.Empty);
    }
}