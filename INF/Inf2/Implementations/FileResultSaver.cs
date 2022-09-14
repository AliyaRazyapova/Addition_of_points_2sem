using Inf2.Abstractions;
using Inf2.Model;

namespace Inf2.Implementations;

/// <summary>
/// Реализация сохранения результатов в файл
/// </summary>
public class FileResultSaver : IResultsSaver
{
    private readonly Config _config;
    private readonly IResultFormatter _resultFormatter;

    public FileResultSaver(Config config, IResultFormatter resultFormatter)
    {
        _config = config;
        _resultFormatter = resultFormatter;
    }

    /// <inheritdoc />
    public void SaveAll(IEnumerable<StudentAssignmentResult> results)
    {
        foreach (var result in results)
        {
            SaveResult(result);
        }
    }

    /// <inheritdoc />
    public void SaveResult(StudentAssignmentResult result)
    {
        var fileName = Path.Join(_config.ResultsFolder, result.Student.Name + ".txt");
        using var file = File.CreateText(fileName);
        file.Write(_resultFormatter.SerializeResults(result));
    }
}