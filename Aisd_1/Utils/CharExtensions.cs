namespace Aisd_1.Utils;

public static class CharExtensions
{
    public static bool IsNumeric(this char input) => char.IsDigit(input);

    public static bool IsAlphabetic(this char input) => char.IsLetter(input);

    public static bool IsAlphaNumeric(this char input) => input.IsAlphabetic() || input.IsNumeric();
    
    public static bool IsWhitespace(this char input) => char.IsWhiteSpace(input);
}