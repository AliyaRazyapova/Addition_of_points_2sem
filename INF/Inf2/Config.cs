namespace Inf2;

/// <summary>
/// Конфигурация
/// </summary>
public class Config
{
    private readonly int _threadCount = 4;

    public Config()
    {
        VariantsFolder = Path.Join(BaseFolder, "Variants", "\\");
        TestsFolder = Path.Join(BaseFolder, "Tests", "\\");
        StudentsFolder = Path.Join(BaseFolder, "Students", "\\");
        ResultsFolder = Path.Join(BaseFolder, "Results", "\\");
        ExercisesFolder = Path.Join(BaseFolder, "Exercises", "\\");
    }

    /// <summary>
    /// Количество потоков
    /// </summary>
    /// <exception cref="ArgumentException">Потоки меньше 0</exception>
    public int ThreadCount
    {
        get => _threadCount;
        init => _threadCount = value > 0
            ? value
            : throw new ArgumentException("Потоки <=0");
    }

    /// <summary>
    /// Папка с упражнениями
    /// </summary>
    public string ExercisesFolder { get; init; }

    /// <summary>
    /// Папка с результатами
    /// </summary>
    public string ResultsFolder { get; init; }

    /// <summary>
    /// Папка с работами студентов
    /// </summary>
    public string StudentsFolder { get; init; }

    /// <summary>
    /// Папка с тестами
    /// </summary>
    public string TestsFolder { get; init; }

    /// <summary>
    /// Папка с вариантами
    /// </summary>
    public string VariantsFolder { get; init; }

    /// <summary>
    /// Базовая директория
    /// </summary>
    public string BaseFolder { get; init; } = Directory.GetCurrentDirectory();
}