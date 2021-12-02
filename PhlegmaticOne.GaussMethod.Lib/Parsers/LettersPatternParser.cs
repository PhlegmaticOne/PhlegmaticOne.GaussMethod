using PhlegmaticOne.GaussMethod.Lib.Constants;
using PhlegmaticOne.GaussMethod.Lib.Extensions;
using PhlegmaticOne.GaussMethod.Lib.Models;
using System.Collections;
using System.Text.RegularExpressions;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;
/// <summary>
/// Represents instance which parses matrix from elements like 4a, -5b, 2.233c etc
/// </summary>
public class LettersPatternParser : MatrixParserBase
{
    public override Regex CoefficientWithVariablePattern => new(@"^-?(\d)+\*?[a-zA-Z]$");
    public override IEnumerable TryParse(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix)
    {
        var variableNames = Regexes.LETTERS_REGEX.Matches(matrixRepresentation.First()).Select(x => x.Value.First());
        var matrixCarcass = new List<IEnumerable<double>>();
        foreach (var row in matrixRepresentation)
        {
            var variableNamesInRow = Regexes.LETTERS_REGEX.Matches(row).Select(x => x.Value.First());
            if (variableNamesInRow.Except(variableNames).Any())
            {
                throw new InvalidOperationException();
            }
            var variablesWithCoefficients = row.Split(' ');
            var rowAnswer = Convert.ToDouble(variablesWithCoefficients.Last());
            var coefficientsInRow = variablesWithCoefficients
                                                     .OrderBy(x => x.Last()).Skip(1)
                                                     .Select(x => Regexes.DIGITS_REGEX.Matches(x).Select(y => Convert.ToDouble(y.Value)))
                                                     .Spread().Append(rowAnswer);
            matrixCarcass.Add(coefficientsInRow);
        }
        extendedSystemMatrix = new ExtendedSystemMatrix(matrixCarcass);
        return variableNames;
    }
}