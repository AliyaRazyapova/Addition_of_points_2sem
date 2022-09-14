namespace Inf2.Utils;

public static class StringExtensions
{
    /// <summary>
    /// Раздилить на строки
    /// </summary>
    /// <param name="str">Строка</param>
    /// <returns>Массив строк</returns>
    /// <remarks>Агностичен к CRLF и LF</remarks>
    public static string[] SplitByLines(this string str) =>
        str.Split(
            new string[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
        );
}