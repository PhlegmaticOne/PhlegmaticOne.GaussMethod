using System.Collections;
using System.Text.RegularExpressions;
using PhlegmaticOne.GaussMethod.Lib.Constants;
using PhlegmaticOne.GaussMethod.Lib.Extensions;
using PhlegmaticOne.GaussMethod.Lib.Models;

namespace PhlegmaticOne.GaussMethod.Lib.Parsers;

public class OneUnknownVariableParser : MatrixParserBase
{
    public override Regex CoefficientWithVariablePattern => new(@"^-?(\d)+\*?[a-zA-Z](\d)+$");
    public override IEnumerable TryParse(IEnumerable<string> matrixRepresentation, out ExtendedSystemMatrix extendedSystemMatrix)
    {
        var variableNames = Regexes.VARIABLE_WITH_DIGITS_REGEX.Matches(matrixRepresentation.First()).Select(x => x.Value);
        var matrixCarcass = new List<IEnumerable<double>>();
        foreach (var row in matrixRepresentation)
        {
            var variableNamesInRow = Regexes.VARIABLE_WITH_DIGITS_REGEX.Matches(row).Select(x => x.Value);
            if (variableNamesInRow.Except(variableNames).Any())
            {
                throw new InvalidOperationException();
            }
            var variablesWithCoefficients = row.Split(' ');
            var rowAnswer = Convert.ToDouble(variablesWithCoefficients.Last());
            var variables = variablesWithCoefficients
                                            .SkipLast(1)
                                            .OrderBy(x => Regexes.VARIABLE_WITH_DIGITS_REGEX.Match(x).Value)
                                            .Select(x => Regexes.VARIABLE_WITH_DIGITS_REGEX.Replace(x, ""))
                                            .Select(x => Convert.ToDouble(x))
                                            .Append(rowAnswer);
            matrixCarcass.Add(variables);
        }
        extendedSystemMatrix = new ExtendedSystemMatrix(matrixCarcass);
        return variableNames;
    }
}