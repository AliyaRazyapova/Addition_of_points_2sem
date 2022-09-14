namespace Inf2.Model;

/// <summary>
/// Статус загрузки сборки
/// </summary>
public enum AssemblyFetchStatus
{
    /// <summary>Загружена</summary>
    Ok,
    /// <summary>Не найдена</summary>
    NotFound,
    /// <summary>Ошибка при загрузке</summary>
    Exception,
}