using System.Text;

namespace Inf2.Model;

/// <summary>
/// Назначение для студента
/// </summary>
/// <param name="Student">Студент</param>
/// <param name="Test">Назначение студента</param>
public record StudentAssignment(
    Student Student,
    Test[] Test);