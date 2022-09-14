using Inf2.Abstractions;
using Inf2.Model;

namespace Inf2.Implementations;

/// <inheritdoc />
public class StudentAssignmentRegistry : IStudentAssignmentRegistry
{
    private readonly Config _config;
    private readonly ITestRegistry _testRegistry;
    private List<StudentAssignment> _assignments;

    /// <inheritdoc />
    public IReadOnlyList<StudentAssignment> Assignments => _assignments;

    public StudentAssignmentRegistry(Config config, ITestRegistry testRegistry)
    {
        _config = config;
        _testRegistry = testRegistry;
    }

    /// <inheritdoc />
    public void LoadAssignments()
    {
        var path = _config.VariantsFolder;
        _assignments = Directory.GetFiles(path)
            .Select(
                x => new StudentAssignment(
                    new Student(Path.GetFileNameWithoutExtension(x)),
                    File.ReadAllLines(x).Select(x => _testRegistry.GetByName(x)).ToArray()))
            .ToList();
    }
}