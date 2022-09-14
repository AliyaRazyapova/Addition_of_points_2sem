using Inf2.Abstractions;
using Inf2.Model;
using Inf2.Utils;

namespace Inf2.Implementations;

/// <summary>
/// Паралельный тестер
/// </summary>
public class ParallelTestWorker : ITestWorker
{
    private readonly Config _config;
    private readonly ITestRunner _runner;

    public ParallelTestWorker(Config config, ITestRunner runner)
    {
        _config = config;
        _runner = runner;
    }

    /// <inheritdoc />
    public StudentAssignmentResult[] Compute(StudentAssignment[] assignments)
    {
        var chunks = assignments.ToChunks(_config.ThreadCount);
        var result = new List<StudentAssignmentResult>();
        var lockHandle = new object();

        Parallel.ForEach(chunks, (x) =>
        {
            var res = x.Select(y => _runner.Check(y));
            lock (lockHandle)
            {
                result.AddRange(res);
            }
        });

        return result.ToArray();
    }
}