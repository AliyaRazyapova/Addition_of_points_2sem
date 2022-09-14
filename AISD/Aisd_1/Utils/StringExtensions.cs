namespace Aisd_1.Utils;

public static class StringExtensions
{
    public static bool IsNumeric(this string input)
        => input.All(x => x.IsNumeric());


    public static bool IsAlphabetic(this string input)
        => input.All(x => x.IsAlphabetic());


    public static bool IsAlphaNumeric(this string input) 
        => input.All(x => x.IsAlphaNumeric());


    public static bool IsVariable(this string input)
        => input[0].IsAlphabetic() && input.IsAlphaNumeric();
}