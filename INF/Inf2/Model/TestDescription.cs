namespace Inf2.Model;

/// <summary>
/// Описание теста
/// </summary>
/// <param name="FileName">Название файла</param>
/// <param name="TaskName">Название теста</param>
/// <param name="ClassName">Название класса</param>
/// <param name="MethodName">Название метода</param>
/// <param name="Arguments">Описание аргументов класса</param>
/// <param name="ReturnType">Возвращаемое значение</param>
public record TestDescription(
    string FileName,
    string TaskName,
    string ClassName,
    string MethodName,
    Dictionary<string, Type> Arguments,
    Type ReturnType);