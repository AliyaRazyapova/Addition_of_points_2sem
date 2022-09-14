using Inf2.Model;

namespace Inf2.Abstractions;

/// <summary>
/// Список заданий студентов
/// </summary>
public interface IStudentAssignmentRegistry
{
    /// <summary>
    /// Список
    /// </summary>
    public IReadOnlyList<StudentAssignment> Assignments { get; }

    /// <summary>
    /// Загрузить задания
    /// </summary>
    void LoadAssignments();
}