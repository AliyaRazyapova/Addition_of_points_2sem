using Inf2.Abstractions;
using Inf2.Model;

namespace Inf2.Implementations;

/// <inheritdoc />
public class TestRegistry : ITestRegistry
{
    private readonly Config _config;
    private readonly ITestParser _parser;

    private List<Test> _loadedTests;

    /// <inheritdoc />
    public IReadOnlyList<Test> LoadedTests => _loadedTests;

    public TestRegistry(Config config, ITestParser parser)
    {
        _config = config;
        _parser = parser;
    }

    /// <inheritdoc />
    public Test? GetByName(string name)
        => _loadedTests.FirstOrDefault(t => t.TestDescription.FileName == name);

    /// <inheritdoc />
    public void LoadTests()
    {
        var path = _config.ExercisesFolder;
        var descriptions = Directory.GetFiles(path)
            .Select(x => _parser.ParseDescription(x, File.ReadAllText(x)))
            .ToList();

        _loadedTests = descriptions.Select(x => new Test(x, GetTestCases(x)))
            .ToList();
    }

    private TestCase[] GetTestCases(TestDescription description)
    {
        var path = Path.Join(_config.TestsFolder, description.FileName);
        return Directory.GetFiles(path)
            .Select(x => _parser.ParseCase(x, description, File.ReadAllText(x)))
            .ToArray();
    }
}