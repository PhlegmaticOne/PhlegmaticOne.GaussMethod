using System.Text.RegularExpressions;

namespace PhlegmaticOne.GaussMethod.Lib.Constants;
/// <summary>
/// Constant regexes for reading and parsing extended matrix system from file
/// </summary>
public static class Regexes
{
    /// <summary>
    /// Regex for only letters
    /// </summary>
    public static readonly Regex LETTERS_REGEX = new("[a-zA-Z]");
    /// <summary>
    /// Regex for only digits
    /// </summary>
    public static readonly Regex DIGITS_REGEX = new(@"-?(\d)+");
    /// <summary>
    /// Regex for variables and numbers
    /// </summary>
    public static readonly Regex VARIABLE_WITH_DIGITS_REGEX = new(@"[a-zA-Z](\d)+");
}