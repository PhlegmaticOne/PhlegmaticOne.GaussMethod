using System.Text.RegularExpressions;

namespace PhlegmaticOne.GaussMethod.Lib.Constants;

public static class Regexes
{
    public static readonly Regex LETTERS_REGEX = new("[a-zA-Z]");
    public static readonly Regex DISGITS_REGEX = new(@"-?(\d)+");
    public static readonly Regex VARIABLE_WITH_DIGITS_REGEX = new(@"[a-zA-Z](\d)+");
}