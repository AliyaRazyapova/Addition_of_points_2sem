using System.Reflection;

namespace Inf2.Utils;

public static class MethodInfoExtensions
{
    /// <summary>
    /// Вызвать метод с параметрами в виде словаря
    /// </summary>
    /// <param name="methodInfo">Вызываемый метод</param>
    /// <param name="obj">this</param>
    /// <param name="parameterValues">Параметры</param>
    /// <returns>Результат</returns>
    /// <exception cref="ArgumentException"></exception>
    public static object? InvokeWithDictionary(
        this MethodInfo methodInfo,
        object obj,
        IDictionary<string, object> parameterValues)
    {
        var parameters = methodInfo.GetParameters()
            .Select(
                p => new
                {
                    Name = p.Name,
                    IsOptional = p.IsOptional
                })
            .Select(
                x =>
                {
                    if (parameterValues.TryGetValue(x.Name, out var value))
                        return value;
                    if (x.IsOptional)
                        return Type.Missing;
                    throw new ArgumentException($"Параметр {x.Name} обязателен, но не найден");
                })
            .ToArray();

        return methodInfo.Invoke(obj, parameters);
    }
}