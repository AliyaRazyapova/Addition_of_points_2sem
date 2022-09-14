using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Inf2.Utils;

public static class EnumExtensions
{
    /// <summary>
    /// Получить DisplayName у Enum
    /// </summary>
    /// <param name="enumValue">Значение enum</param>
    /// <returns>Назване</returns>
    public static string? GetDisplayName(this Enum enumValue)
        => enumValue.GetType()
            .GetMember(enumValue.ToString())
            .First()
            .GetCustomAttribute<DisplayAttribute>()
            ?.Name;
}