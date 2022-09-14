using Inf2.Abstractions;
using Inf2.Model;
using Inf2.Utils;

namespace Inf2.Implementations;

/// <inheritdoc />
public class TestParser : ITestParser
{
    private readonly IParameterTypeRegistry _parameterTypeRegistry;

    public TestParser(IParameterTypeRegistry parameterTypeRegistry)
    {
        _parameterTypeRegistry = parameterTypeRegistry;
    }

    /// <inheritdoc />
    public TestDescription ParseDescription(string filePath, string fileContent)
    {
        var lines = fileContent.SplitByLines();
        if (lines.Length < 4)
            throw new ArgumentException($"В задании {filePath} не хватает определений");

        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var testName = lines[0];
        var classAndMethod = lines[1].Split('.');
        var className = classAndMethod[0];
        var methodName = classAndMethod[1];
        var returnType = _parameterTypeRegistry.GetTypeByName(lines[2])
                         ?? throw new ArgumentException(
                             $"Неизвестный тип возвращаемого значения {lines[2]}",
                             nameof(fileName));
        var args = lines.Skip(3).Select(
            x =>
            {
                var nameAndType = x.Split(":");
                return new
                {
                    Name = nameAndType[0],
                    Type = _parameterTypeRegistry.GetTypeByName(nameAndType[1])
                           ?? throw new ArgumentException(
                               $"Неизвестный тип {nameAndType[1]} для {nameAndType[0]}",
                               nameof(fileName))
                };
            }).ToDictionary(x => x.Name, x => x.Type);

        return new TestDescription(
            fileName,
            testName,
            className,
            methodName,
            args,
            returnType);
    }

    /// <inheritdoc />
    public TestCase ParseCase(
        string fileName,
        TestDescription testDescription,
        string fileContent)
    {
        var lines = fileContent.SplitByLines();
        var caseName = Path.GetFileNameWithoutExtension(fileName);
        var expected = _parameterTypeRegistry.Parse(
            testDescription.ReturnType,
            lines[^1]);

        var args = lines.SkipLast(1).Select(
                x =>
                {
                    var nameAndValue = x.Split(":");
                    var type = testDescription.Arguments.TryGetValue(nameAndValue[0], out var argType)
                        ? argType
                        : throw new ArgumentException(
                            $"Параметр {nameAndValue[0]} не описан в задании, но встречен в {fileName}");

                    return new
                    {
                        Name = nameAndValue[0],
                        Value = _parameterTypeRegistry.Parse(
                            argType,
                            nameAndValue[1])
                    };
                })
            .ToDictionary(x => x.Name, x => x.Value);

        return new TestCase(
                caseName,
                args,
                expected);
    }
}