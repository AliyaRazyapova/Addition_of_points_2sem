using System.ComponentModel.DataAnnotations;
using Inf2.Utils;

namespace Inf2.Model;

/// <summary>
/// Результаты теста
/// </summary>
public enum TestCaseResults
{
    /// <summary>Успешно</summary>
    [Display(Name = "Успешно")]
    Success,

    /// <summary>Неправильный ответ</summary>
    [Display(Name = "Неправильный ответ")]
    WrongAnswer,

    /// <summary>Ошибка во время выполнения</summary>
    [Display(Name = "Ошибка во время выполнения")]
    UnhandledException,

    /// <summary> Несоответствие заданию </summary>
    [Display(Name = "Несоответствие заданию (не найден метод)")]
    MismatchedName,

    /// <summary>Не выполнено (не найдена сборка)</summary>
    [Display(Name = "Не выполнено (не найдена сборка)")]
    AssemblyNotFound,

    /// <summary>
    /// Не протестированно
    /// </summary>
    [Display(Name = "Не протестированно")] Untested
}

public record TestCaseResult(
    TestCase TestCase,
    TestCaseResults CaseResult,
    Exception? Ex = null,
    string Message = "")
{
    public bool IsFailed => CaseResult != TestCaseResults.Success;

    public override string ToString()
        => $"{TestCase.Name}: {CaseResult.GetDisplayName()}. {Message}"
           + (Ex is null ? "" : $" Исключение: {Ex.GetType()}");
};